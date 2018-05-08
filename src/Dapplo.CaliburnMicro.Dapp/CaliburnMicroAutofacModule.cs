using Autofac;
using Caliburn.Micro;
using Dapplo.Addons;
using Dapplo.CaliburnMicro.Dapp.Services;

namespace Dapplo.CaliburnMicro.Dapp
{
    /// <inheritdoc />
    public class CaliburnMicroAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UiStartupShutdown>()
                .As<IStartupMarker>()
                .As<IShutdownMarker>()
                .SingleInstance();


            // Export the DapploWindowManager if no other IWindowManager is registered
            builder.RegisterType<DapploWindowManager>()
                .As<IWindowManager>()
                .IfNotRegistered(typeof(IWindowManager))
                .SingleInstance();

            // Export the EventAggregator if no other IEventAggregator is registered
            builder.RegisterType<EventAggregator>()
                .As<IEventAggregator>()
                .IfNotRegistered(typeof(IEventAggregator))
                .SingleInstance();
        }
    }
}
