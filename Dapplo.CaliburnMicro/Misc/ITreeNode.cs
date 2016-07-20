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

using System.Collections.Generic;

#endregion

namespace Dapplo.CaliburnMicro.Misc
{
	/// <summary>
	///     Interface for tree nodes
	/// </summary>
	/// <typeparam name="TTreeItem"></typeparam>
	public interface ITreeNode<TTreeItem>
	{
		/// <summary>
		///     The parent for this ITreeNode
		/// </summary>
		ITreeNode<TTreeItem> ParentNode { get; set; }

		/// <summary>
		///     The children for this ITreeNode
		/// </summary>
		ICollection<ITreeNode<TTreeItem>> ChildNodes { get; set; }
	}
}