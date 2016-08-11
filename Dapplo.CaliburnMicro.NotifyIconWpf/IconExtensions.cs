﻿#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.CaliburnMicro
// 
// Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
	/// <summary>
	///     Extension method to support Icon conversion
	/// </summary>
	public static class IconExtensions
	{
		/// <summary>
		/// GetSystemMetrics
		/// </summary>
		/// <param name="smIndex">SystemMetric:int</param>
		/// <returns>int with requested value</returns>
		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int smIndex);

		/// <summary>
		/// The default width of an icon, in pixels. The LoadIcon function can load only icons with the dimensions 
		/// that SM_CXICON and SM_CYICON specifies.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		private const int SM_CXICON = 11;

		/// <summary>
		///     Render the frameworkElement to a "GDI" Icon with the default size.
		/// </summary>
		/// <param name="frameworkElement">FrameworkElement</param>
		/// <param name="size">Optional, specifies the size, if not given the system default is used</param>
		/// <returns>Icon</returns>
		public static Icon ToIcon(this FrameworkElement frameworkElement, int? size = null)
		{
			if (frameworkElement == null)
			{
				throw new ArgumentNullException(nameof(frameworkElement));
			}
			int iconSize = size ?? GetSystemMetrics(SM_CXICON);
			var stream = new MemoryStream();
			frameworkElement.WriteAsIconToStream(stream, new []{ iconSize });
			// Reset the stream position, otherwise the icon won't be read correctly (pointer is behind the icon)
			stream.Seek(0, SeekOrigin.Begin);
			return new Icon(stream, iconSize, iconSize);
		}

		/// <summary>
		/// Create a "GDI" icon from the supplied FrameworkElement, it is possible to specify multiple icon sizes.
		/// Note: this doesn't work on Windows versions BEFORE Windows Vista!
		/// </summary>
		/// <param name="frameworkElement">FrameworkElement to convert to an icon</param>
		/// <param name="stream">Stream to write to</param>
		/// <param name="optionalIconSizes">Optional, IEnumerable with icon sizes, default Icon sizes (as specified by windows): 16x16, 32x32, 48x48, 256x256</param>
		public static void WriteAsIconToStream(this FrameworkElement frameworkElement, Stream stream, IEnumerable<int> optionalIconSizes = null)
		{
			if (frameworkElement == null)
			{
				throw new ArgumentNullException(nameof(frameworkElement));
			}
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}

			// Use the supplied or default values for the icon sizes
			var iconSizes = optionalIconSizes?.ToList() ?? new List<int> {16, 32, 48, 256};
			
			var binaryWriter = new BinaryWriter(stream);
			//
			// ICONDIR structure
			//
			binaryWriter.Write((short)0); // reserved
			binaryWriter.Write((short)1); // image type (icon)
			binaryWriter.Write((short)iconSizes.Count); // number of images

			IList<Size> imageSizes = new List<Size>();
			IList<MemoryStream> encodedImages = new List<MemoryStream>();
			foreach (int iconSize in iconSizes)
			{
				var currentSize = new Size(iconSize, iconSize);
				// Store the size for later
				imageSizes.Add(currentSize);
				var bitmapSource = frameworkElement.ToBitmapSource(currentSize);

				// No need to dispse the memorystream as the created tmpBitmap "owns" the stream and already disposes it.
				var imageStream = new MemoryStream();
				// Use PngBitmapEncoder for icons, with this we also respect transparency.
				var encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
				encoder.Save(imageStream);
				// Make sure the stream is read from the beginning
				imageStream.Seek(0, SeekOrigin.Begin);
				// Store the stream for later
				encodedImages.Add(imageStream);
			}

			//
			// ICONDIRENTRY structure
			//
			const int iconDirSize = 6;
			const int iconDirEntrySize = 16;

			var offset = iconDirSize + (imageSizes.Count * iconDirEntrySize);
			for (int i = 0; i < imageSizes.Count; i++)
			{
				var imageSize = imageSizes[i];
				// Write the width / height, 0 means 256
				binaryWriter.Write((int)imageSize.Width == 256 ? (byte)0 : (byte)imageSize.Width);
				binaryWriter.Write((int)imageSize.Height == 256 ? (byte)0 : (byte)imageSize.Height);
				binaryWriter.Write((byte)0); // no pallete
				binaryWriter.Write((byte)0); // reserved
				binaryWriter.Write((short)0); // no color planes
				binaryWriter.Write((short)32); // 32 bpp
				binaryWriter.Write((int)encodedImages[i].Length); // image data length
				binaryWriter.Write(offset);
				offset += (int)encodedImages[i].Length;
			}

			binaryWriter.Flush();
			//
			// Write image data
			//
			foreach (var encodedImage in encodedImages)
			{
				encodedImage.WriteTo(stream);
				encodedImage.Dispose();
			}
		}

		/// <summary>
		///     Render the frameworkElement to a BitmapSource
		/// </summary>
		/// <param name="frameworkElement">FrameworkElement</param>
		/// <param name="size">Size, using the bound as size by default</param>
		/// <param name="dpiX">Horizontal DPI settings</param>
		/// <param name="dpiY">Vertical DPI settings</param>
		/// <returns>BitmapSource</returns>
		public static BitmapSource ToBitmapSource(this FrameworkElement frameworkElement, Size? size = null, double dpiX = 96.0, double dpiY = 96.0)
		{
			if (frameworkElement == null)
			{
				throw new ArgumentNullException(nameof(frameworkElement));
			}
			// Make sure we have a size
			if (!size.HasValue)
			{
				var bounds = VisualTreeHelper.GetDescendantBounds(frameworkElement);
				size = bounds != Rect.Empty ? bounds.Size : new Size(16, 16);
			}

			// Create a viewbox to render the frameworkElement in the correct size
			var viewbox = new Viewbox
			{
				//frameworkElement to render
				Child = frameworkElement
			};
			viewbox.Measure(size.Value);
			viewbox.Arrange(new Rect(new Point(), size.Value));
			viewbox.UpdateLayout();

			var renderTargetBitmap = new RenderTargetBitmap((int) (size.Value.Width*dpiX/96.0),
				(int) (size.Value.Height*dpiY/96.0),
				dpiX,
				dpiY,
				PixelFormats.Pbgra32);
			var drawingVisual = new DrawingVisual();
			using (var drawingContext = drawingVisual.RenderOpen())
			{
				var visualBrush = new VisualBrush(viewbox);
				drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(), size.Value));
			}
			renderTargetBitmap.Render(drawingVisual);
			// Disassociate the frameworkElement from the viewbox
			viewbox.RemoveChild(frameworkElement);
			return renderTargetBitmap;
		}

		/// <summary>
		/// Disassociate the child from the parent
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		private static void RemoveChild(this DependencyObject parent, UIElement child)
		{
			var panel = parent as Panel;
			if (panel != null)
			{
				panel.Children.Remove(child);
				return;
			}

			var decorator = parent as Decorator;
			if (decorator != null)
			{
				if (ReferenceEquals(decorator.Child, child))
				{
					decorator.Child = null;
				}
				return;
			}

			var contentPresenter = parent as ContentPresenter;
			if (contentPresenter != null)
			{
				if (Equals(contentPresenter.Content, child))
				{
					contentPresenter.Content = null;
				}
				return;
			}

			var contentControl = parent as ContentControl;
			if (contentControl != null)
			{
				if (Equals(contentControl.Content, child))
				{
					contentControl.Content = null;
				}
			}
		}
	}
}