﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2018 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.CaliburnMicro
// 
//  Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac.Features.AttributeFilters;
using Dapplo.CaliburnMicro.Overlays;
using Dapplo.CaliburnMicro.Overlays.ViewModels;

namespace Application.Demo.OverlayAddon.ViewModels
{
    /// <summary>
    /// This is the view model which will display all IOverlay items.
    /// If you want, you can have the overlay extend IActivate to get called when it's activated.
    /// </summary>
    [SuppressMessage("Sonar Code Smell", "S110:Inheritance tree of classes should not be too deep", Justification = "MVVM Framework brings huge interitance tree.")]
    public sealed class DemoOverlayContainerViewModel : OverlayContainerViewModel
    {
        private readonly IEnumerable<Lazy<IOverlay>> _overlays;

        public DemoOverlayContainerViewModel(
            [MetadataFilter("Overlay", "demo")]
            IEnumerable<Lazy<IOverlay>> overlays
        )
        {
            _overlays = overlays;
        }
        /// <summary>
        /// Make sure all the items are assigned
        /// </summary>
        protected override void OnActivate()
        {
            Items.AddRange(_overlays.Select(lazy => lazy.Value));
            base.OnActivate();
        }
    }
}
