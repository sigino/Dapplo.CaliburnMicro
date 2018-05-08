﻿using Autofac;
using Caliburn.Micro;
using Dapplo.CaliburnMicro.Configurers;

namespace Dapplo.CaliburnMicro
{
    /// <inheritdoc />
    public class CaliburnMicroAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CultureViewConfigurer>()
                .As<IConfigureDialogViews>()
                .As<IConfigureWindowViews>()
                .SingleInstance();
            builder.RegisterType<DpiAwareViewConfigurer>()
                .As<IConfigureDialogViews>()
                .As<IConfigureWindowViews>()
                .SingleInstance();
            builder.RegisterType<IconViewConfigurer>()
                .As<IConfigureDialogViews>()
                .As<IConfigureWindowViews>()
                .SingleInstance();
            builder.RegisterType<PlacementViewConfigurer>()
                .As<IConfigureDialogViews>()
                .As<IConfigureWindowViews>()
                .SingleInstance();
            builder.RegisterType<DapploWindowManager>()
                .As<IWindowManager>()
                .SingleInstance();
        }
    }
}
