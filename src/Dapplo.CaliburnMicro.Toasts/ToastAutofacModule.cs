using Autofac;

namespace Dapplo.CaliburnMicro.Toasts
{
    /// <inheritdoc />
    public class ToastAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToastConductor>()
                .As<IUiStartup>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
