using Application.Demo.ClickOnce.ViewModels;
using Autofac;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.ClickOnce;

namespace Application.Demo.ClickOnce
{
    public class ClickOnceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HandleClickOnceRestarts>()
                .As<IHandleClickOnceRestarts>()
                .SingleInstance();

            builder.RegisterType<AboutViewModel>()
                .As<IShell>()
                .SingleInstance();
        }
    }
}
