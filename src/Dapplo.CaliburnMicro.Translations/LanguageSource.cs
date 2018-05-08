using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Builder;
using Autofac.Core;
using Dapplo.Language;

namespace Dapplo.CaliburnMicro.Translations
{
    /// <inheritdoc />
    /// <summary>
    /// This is a registration source for the interfaces extending ILanguage.
    /// </summary>
    public class LanguageSource : IRegistrationSource
    {
        private static readonly MethodInfo BuildMethod = typeof(LanguageSource).GetMethod(nameof(BuildRegistration), BindingFlags.Static | BindingFlags.NonPublic);

        /// <inheritdoc />
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc />
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts == null || !typeof(ILanguage).IsAssignableFrom(ts.ServiceType))
            {
                return Enumerable.Empty<IComponentRegistration>();
            }

            var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
            return new []{(IComponentRegistration)buildMethod.Invoke(null, null) };
        }

        /// <summary>
        /// This creates a component registration for the specified type
        /// </summary>
        /// <typeparam name="TLanguage">interface extending ILanguage</typeparam>
        /// <returns>IComponentRegistration</returns>
        private static IComponentRegistration BuildRegistration<TLanguage>() where TLanguage : ILanguage
        {
            return RegistrationBuilder
                .ForDelegate((c, p) => LanguageLoader.Current.Get<TLanguage>())
                .CreateRegistration();
        }
    }
}
