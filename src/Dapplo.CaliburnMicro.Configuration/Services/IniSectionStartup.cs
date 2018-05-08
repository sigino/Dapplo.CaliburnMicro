using System.Threading;
using System.Threading.Tasks;
using Dapplo.Addons;
using Dapplo.Ini;

namespace Dapplo.CaliburnMicro.Configuration.Services
{
    /// <summary>
    /// This makes sure the configuration is loaded
    /// </summary>
    [StartupOrder(int.MinValue), ShutdownOrder(int.MaxValue)]
    public class IniSectionStartup : IStartupAsync, IShutdownAsync
    {
        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            return IniConfig.Current.LoadIfNeededAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task ShutdownAsync(CancellationToken cancellationToken = default)
        {
            return IniConfig.Current.WriteAsync(cancellationToken);
        }
    }
}
