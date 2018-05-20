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

#region using

using System.Collections.Generic;
using Dapplo.CaliburnMicro.Security;
using Dapplo.CaliburnMicro.Security.Behaviors;

#endregion

namespace Dapplo.CaliburnMicro.Configuration
{
    /// <summary>
    ///     An extension of the ConfigScreen which adds authentication
    /// </summary>
    public abstract class AuthenticatedConfigScreen<TWhen> : ConfigScreen, IChangeableNeedAuthentication<TWhen>
    {
        private AuthenticationTargetProperties _authenticationTargetProperty = AuthenticationTargetProperties.None;
        private PermissionOperations _permissionOperation = PermissionOperations.Or;
        private IEnumerable<string> _permissions = new List<string>();
        private TWhen _whenPermission;
        private TWhen _whenPermissionMissing;

        /// <inheritdoc />
        public AuthenticationTargetProperties AuthenticationTargetProperty
        {
            get => _authenticationTargetProperty;
            set
            {
                _authenticationTargetProperty = value;
                NotifyOfPropertyChange(nameof(AuthenticationTargetProperty));
            }
        }

        /// <inheritdoc />
        public PermissionOperations PermissionOperation
        {
            get => _permissionOperation;
            set
            {
                _permissionOperation = value;
                NotifyOfPropertyChange(nameof(PermissionOperation));
            }
        }


        /// <inheritdoc />
        public IEnumerable<string> Permissions
        {
            get => _permissions;
            set
            {
                _permissions = value;
                NotifyOfPropertyChange(nameof(Permissions));
            }
        }

        /// <inheritdoc />
        public TWhen WhenPermission
        {
            get => _whenPermission;
            set
            {
                _whenPermission = value;
                NotifyOfPropertyChange(nameof(WhenPermission));
            }
        }

        /// <inheritdoc />
        public TWhen WhenPermissionMissing
        {
            get => _whenPermissionMissing;
            set
            {
                _whenPermissionMissing = value;
                NotifyOfPropertyChange(nameof(WhenPermissionMissing));
            }
        }
    }
}