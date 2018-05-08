using Autofac;
using Dapplo.Addons;
using Dapplo.CaliburnMicro.Translations.Services;
using Dapplo.Language;

namespace Dapplo.CaliburnMicro.Translations
{
    /// <inheritdoc />
    public class RegisterLanguageModule : Module
    {
        private LanguageLoader _languageLoader;

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LanguageStartup>()
                .As<IStartupMarker>()
                .SingleInstance();

            var applicationName = builder.Properties["applicationName"] as string;

            _languageLoader = LanguageLoader.Current;
            if (_languageLoader == null)
            {
                _languageLoader = LanguageLoader.Current ?? new LanguageLoader(applicationName);
            }
            builder.RegisterSource(new LanguageSource());
        }
    }
}
