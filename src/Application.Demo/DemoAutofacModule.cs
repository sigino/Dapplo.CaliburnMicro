using Application.Demo.Services;
using Application.Demo.UseCases.Configuration.ViewModels;
using Application.Demo.UseCases.ContextMenu.ViewModels;
using Application.Demo.UseCases.Menu.ViewModels;
using Application.Demo.UseCases.Toast.ViewModels;
using Application.Demo.UseCases.Wizard.ViewModels;
using Application.Demo.ViewModels;
using Autofac;
using Autofac.Features.AttributeFilters;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.CaliburnMicro.NotifyIconWpf;
using Dapplo.CaliburnMicro.Security;
using Dapplo.CaliburnMicro.Wizard;
using Dapplo.Log;

namespace Application.Demo
{
    /// <inheritdoc />
    public class DemoAutofacModule : Module
    {
        private static readonly LogSource Log = new LogSource();
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DemoTrayIconViewModel>()
                .As<ITrayIconViewModel>()
                .WithAttributeFiltering()
                .SingleInstance();
            builder.RegisterType<AuthenticationProvider>()
                .As<IAuthenticationProvider>()
                .SingleInstance();
            builder.RegisterType<NotifyOfStartupReady>()
                .As<IUiStartup>()
                .SingleInstance();
            builder.RegisterType<SimpleVersionProvider>()
                .As<IVersionProvider>()
                .SingleInstance();

            // All IMenuItem with the context they belong to
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IMenuItem>()
                .As<IMenuItem>()
                .SingleInstance();

            // All config screens
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IConfigScreen>()
                .As<IConfigScreen>()
                .SingleInstance();

            // All wizard screens
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IWizardScreen>()
                .As<IWizardScreen>()
                .SingleInstance();

            builder.RegisterType<ConfigViewModel>()
                .AsSelf()
                .SingleInstance();

            // View models
            builder.RegisterType<WindowWithMenuViewModel>()
                .AsSelf()
                .WithAttributeFiltering()
                .SingleInstance();

            builder.RegisterType<StartupReadyToastViewModel>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<WizardExampleViewModel>()
                .AsSelf()
                .SingleInstance();
            
            // Not a single instance
            builder.RegisterType<ToastExampleViewModel>()
                .AsSelf();
            builder.RegisterType<NotificationExampleViewModel>()
                .AsSelf();
        }
    }
}
