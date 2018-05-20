using Application.Demo.OverlayAddon.ViewModels;
using Autofac;
using Autofac.Features.AttributeFilters;
using Dapplo.Addons;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.CaliburnMicro.Overlays;
using Dapplo.Log;

namespace Application.Demo.OverlayAddon
{
    /// <inheritdoc />
    public class OverlayAddonModule : AddonModule
    {
        private static readonly LogSource Log = new LogSource();
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OverlayMenuItem>()
                .As<IMenuItem>()
                // Only when scanning the attributes are taken
                .WithMetadata("Context", "contextmenu")
                .SingleInstance();

            // All IOverlay for the demo
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IOverlay>()
                .As<IOverlay>()
                .SingleInstance();

            builder.RegisterType<DemoOverlayContainerViewModel>()
                .WithAttributeFiltering()
                .AsSelf()
                .SingleInstance();
        }
    }
}
