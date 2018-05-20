﻿using Application.Demo.ClickOnce.ViewModels;
using Autofac;
using Dapplo.Addons;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.ClickOnce;

namespace Application.Demo.ClickOnce
{
    public class ClickOnceAddonModule : AddonModule
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
