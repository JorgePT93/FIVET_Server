using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fivet.Server
{
    internal class FivetService : IHostedService
    {
        private readonly ILogger<FivetService> _logger;

        public FivetService(ILogger<FivetService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service
        /// <summary>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ...");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown
        /// <summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the FivetService ...");
            return Task.CompletedTask;
        }
    }
}