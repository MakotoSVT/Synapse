using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Synapse.Providers
{
    public class AlertProvider : BaseProvider, IAlertProvider
    {
        public AlertProvider(IConfiguration configuration, ILogger logger) : base(configuration, logger)
        {

        }
    }
}
