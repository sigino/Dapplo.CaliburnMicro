using Autofac;
using Dapplo.CaliburnMicro.Diagnostics.Translations;
using Dapplo.CaliburnMicro.Diagnostics.ViewModels;

namespace Dapplo.CaliburnMicro.Diagnostics
{
    /// <inheritdoc />
    public class DiagnosticsAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ErrorViewModel
            {
                VersionProvider = context.ResolveOptional<IVersionProvider>(),
                ErrorTranslations = context.ResolveOptional<IErrorTranslations>()
            });

        }
    }
}
