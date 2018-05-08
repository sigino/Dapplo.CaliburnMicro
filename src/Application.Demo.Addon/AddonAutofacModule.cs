using Autofac;
using Dapplo.CaliburnMicro.Configuration;

namespace Application.Demo.Addon
{
    public class AddonAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IConfigScreen>()
                .As<IConfigScreen>()
                .SingleInstance();

        }
    }
}
