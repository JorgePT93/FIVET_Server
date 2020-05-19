using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
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
        /// </summary>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ...");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the FivetService ...");
            return Task.CompletedTask;
        }
    }
    
    /// <summary>
    /// The implementation of TheSystem interface
    /// </summary>
    public class TheSystem : TheSystemDisp_
    {
        /// <summary>
        /// Return the difference in time
        /// </summary>
        /// <param name="clientTime"></param>
        /// <returns>The Delay</returns>
        public override long getDelay(long clientTime, Ice.Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }

    /// <summary>
    /// Build the Communicator
    /// </summary>
    /// <returns> The Communicator </returns>
    private Communicator buildCommunicator()
    {
        _logger.LogDebug("Initializing Communicator v{0} ({1}) ...", Ice.UtilstringVersion(), Ice.Util.intVersion());

        // ZeroC properties
        Properties properties = Util.createProperties();
        // https://doc.zeroc.com/ice/latest/property-reference/ice-trace
        // properties.setProperty("Ice.Trace.Admin.Properties", "1");
        // properties.setProperty("Ice.Trace.Locator", "2");
        // properties.setProperty("Ice.Trace.Network", "3");
        // properties.setProperty("Ice.Trace.Protocol", "1");
        // properties.setProperty("Ice.Trace.Slicing", "1");
        // properties.setProperty("Ice.Trace.ThreadPool", "1");
        // properties.setProperty("Ice.Trace.Level", "9");

        InitializationData initializationData = new InitializationData();
        initializationData.properties = properties;
        
        return Ice.Util.initialize(initializationData);
    }
}