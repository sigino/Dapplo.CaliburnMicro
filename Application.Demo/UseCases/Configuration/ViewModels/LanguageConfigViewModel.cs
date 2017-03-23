﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2017 Dapplo
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using Application.Demo.Languages;
using Application.Demo.Models;
using Caliburn.Micro;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.Language;

#endregion

namespace Application.Demo.UseCases.Configuration.ViewModels
{
    [Export(typeof(IConfigScreen))]
    public sealed class LanguageConfigViewModel : ConfigScreen
    {
        /// <summary>
        ///     Here all disposables are registered, so we can clean the up
        /// </summary>
        private CompositeDisposable _disposables;

        public IDictionary<string, string> AvailableLanguages => LanguageLoader.Current.AvailableLanguages;

        /// <summary>
        ///     Can the login button be pressed?
        /// </summary>
        public bool CanChangeLanguage
            => !string.IsNullOrWhiteSpace(DemoConfiguration.Language) && DemoConfiguration.Language != LanguageLoader.Current.CurrentLanguage;

        [Import]
        public ICoreTranslations CoreTranslations { get; set; }

        [Import]
        public IDemoConfiguration DemoConfiguration { get; set; }

        [Import]
        private IEventAggregator EventAggregator { get; set; }

        public override void Commit()
        {
            // Manually commit
            DemoConfiguration.CommitTransaction();
            EventAggregator.PublishOnUIThread($"Changing to language: {DemoConfiguration.Language}");
            Execute.OnUIThread(async () => { await LanguageLoader.Current.ChangeLanguageAsync(DemoConfiguration.Language).ConfigureAwait(false); });

            base.Commit();
        }

        public override void Initialize(IConfig config)
        {
            // Prepare disposables
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();

            // Place this under the Ui parent
            ParentId = nameof(ConfigIds.Ui);

            // Make sure Commit/Rollback is called on the IDemoConfiguration
            config.Register(DemoConfiguration);

            // automatically update the DisplayName
            _disposables.Add(CoreTranslations.CreateDisplayNameBinding(this, nameof(ICoreTranslations.Language)));

            // automatically update the CanChangeLanguage state when a different language is selected
            _disposables.Add(DemoConfiguration.OnPropertyChanged(nameof(IDemoConfiguration.Language)).Subscribe(pcEvent => NotifyOfPropertyChange(nameof(CanChangeLanguage))));

            base.Initialize(config);
        }

        protected override void OnDeactivate(bool close)
        {
            _disposables.Dispose();
            base.OnDeactivate(close);
        }
    }
}