using Autofac;
using Dapplo.Addons;
using Dapplo.CaliburnMicro.Configuration.Services;
using Dapplo.Ini;

namespace Dapplo.CaliburnMicro.Configuration
{
    /// <inheritdoc />
    public class DapploAddonsModule : Module
    {
        private IniConfig _applicationIniConfig;

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IniSectionStartup>()
                .As<IStartupMarker>()
                .As<IShutdownMarker>()
                .SingleInstance();

            var applicationName = builder.Properties["applicationName"] as string;
            _applicationIniConfig = IniConfig.Current ?? new IniConfig(applicationName, applicationName);
            builder.RegisterSource(new IniSectionSource());
        }
    }
}
