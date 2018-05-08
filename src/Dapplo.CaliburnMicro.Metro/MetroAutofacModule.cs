using Autofac;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;

namespace Dapplo.CaliburnMicro.Metro
{
    /// <inheritdoc />
    public class MetroAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MetroWindowManager>()
                .AsSelf()
                .As<IWindowManager>()
                .SingleInstance();
            
            // Register the IDialogCoordinator of MahApps, so ViewModels can open MahApps dialogs
            builder.RegisterInstance(DialogCoordinator.Instance).As<IDialogCoordinator>().SingleInstance();

        }
    }
}
