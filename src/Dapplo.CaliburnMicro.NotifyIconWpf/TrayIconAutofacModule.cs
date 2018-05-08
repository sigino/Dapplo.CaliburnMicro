using Autofac;
using Dapplo.Addons;

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
    /// <inheritdoc />
    public class TrayIconAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrayIconManager>()
                .As<IStartupMarker>()
                .As<IShutdownMarker>()
                .As<ITrayIconManager>()
                .SingleInstance();
        }
    }
}
