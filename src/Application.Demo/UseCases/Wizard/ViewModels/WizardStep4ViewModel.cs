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

using System;
using Application.Demo.Languages;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Wizard;

#endregion

namespace Application.Demo.UseCases.Wizard.ViewModels
{
    public sealed class WizardStep4ViewModel : WizardScreen<WizardExampleViewModel>
    {
        private IDisposable _displayNameUpdater;

        public WizardStep4ViewModel(IWizardTranslations wizardTranslations)
        {
            WizardTranslations = wizardTranslations;
            Order = 4;
        }
        
        public IWizardTranslations WizardTranslations { get; }

        public override void Initialize()
        {
            // automatically update the DisplayName
            _displayNameUpdater = WizardTranslations.CreateDisplayNameBinding(this, nameof(IWizardTranslations.TitleStep4));

            ParentWizard.OnPropertyChanged(nameof(WizardExampleViewModel.IsStep4Complete)).Subscribe(s => IsComplete = ParentWizard.IsStep4Complete);
            IsComplete = ParentWizard.IsStep4Complete;
        }

        public override void Terminate()
        {
            _displayNameUpdater?.Dispose();
        }
    }
}