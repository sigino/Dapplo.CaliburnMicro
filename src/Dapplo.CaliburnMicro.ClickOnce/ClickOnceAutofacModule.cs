using Autofac;
using Dapplo.Addons;

namespace Dapplo.CaliburnMicro.ClickOnce
{
    /// <inheritdoc />
    public class ClickOnceAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClickOnceService>()
                .As<IStartupMarker>()
                .As<IClickOnceService>()
                .As<IVersionProvider>().SingleInstance();
        }
    }
}
