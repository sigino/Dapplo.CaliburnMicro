using Autofac;

namespace Dapplo.CaliburnMicro.Security.ActiveDirectory
{
    /// <inheritdoc />
    public class ActiveDirectoryAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActiveDirectoryAuthenticationProvider>()
                .As<IAuthenticationProvider>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
