using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Builder;
using Autofac.Core;
using Dapplo.Ini;

namespace Dapplo.CaliburnMicro.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// This is a registration source for the interfaces extending IIniConfig.
    /// </summary>
    public class IniSectionSource : IRegistrationSource
    {
        private static readonly MethodInfo BuildMethod = typeof(IniSectionSource).GetMethod(nameof(BuildRegistration), BindingFlags.Static | BindingFlags.NonPublic);

        /// <inheritdoc />
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc />
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts == null || !typeof(IIniSection).IsAssignableFrom(ts.ServiceType))
            {
                return Enumerable.Empty<IComponentRegistration>();
            }

            var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
            return new []{(IComponentRegistration)buildMethod.Invoke(null, null) };
        }

        /// <summary>
        /// This creates a component registration for the specified type
        /// </summary>
        /// <typeparam name="TIniConfig">interface extending IIniSection</typeparam>
        /// <returns>IComponentRegistration</returns>
        private static IComponentRegistration BuildRegistration<TIniConfig>() where TIniConfig : IIniSection
        {
            return RegistrationBuilder
                .ForDelegate((c, p) => IniConfig.Current.Get<TIniConfig>())
                .CreateRegistration();
        }
    }
}
