using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fivet.Server
{
    /// <summary>
    /// The Fivet Service    
    /// </summary>
    internal class FivetService : IHostedService, IDisposable
    {
        /// <summary>
        /// The Logger    
        /// </summary>
        private readonly ILogger<FivetService> _logger;

        /// <summary>
        /// The Port    
        /// </summary>
        private readonly int _port = 8080;

        /// <summary>
        /// The Communicator    
        /// </summary>
        private readonly Communicator _communicator;

        /// <summary>
        /// The System    
        /// </summary>
        private readonly TheSystemDisp_ _theSystem;

        /// <summary>
        /// The Contratos    
        /// </summary>
        private readonly ContratosDisp_ _contratos;

        /// <summary>
        /// The FivetService    
        /// </summary>
        public FivetService(ILogger<FivetService> logger, TheSystemDisp_ theSystem, ContratosDisp_ contratos)
        {
            _logger = logger;
            _logger.LogDebug("Building FivetService...");
            _theSystem = theSystem;
            _contratos = contratos;
            _communicator = buildCommunicator();
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service
        /// </summary>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ...");

            // The adapter: https://doc.zeroc.com/ice/7.3/client-side-features/proxies/proxy-and-endpoint-syntax
            // tpc(protocol) -z (compression) -t 15000 (timeout in ms) -p 8080 (port to bind)
            var adapter = _communicator.createObjectAdapterWithEndpoints("TheSystem", "tcp -z -t 15000 -p " + _port);

            // Register in the communicator
            adapter.add(_theSystem, Util.stringToIdentity("TheSystem"));

            // Activation
            adapter.activate();

            // All ok
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the FivetService ...");

            _communicator.shutdown();

            _logger.LogDebug("Communicator Stopped ...");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Build the Communicator
        /// </summary>
        /// <returns> The Communicator </returns>
        private Communicator buildCommunicator()
        {
            _logger.LogDebug("Initializing Communicator v{0} ({1}) ...", Ice.Util.stringVersion(), Ice.Util.intVersion());

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
        
        public void Dispose()
        {
            _communicator.destroy();
        }        
    }
}