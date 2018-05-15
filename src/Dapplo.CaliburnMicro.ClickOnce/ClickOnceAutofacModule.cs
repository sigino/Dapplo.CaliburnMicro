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
                .As<IService>()
                .As<IClickOnceService>()
                .As<IVersionProvider>().SingleInstance();
        }
    }
}
