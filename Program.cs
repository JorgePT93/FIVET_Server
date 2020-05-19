using System;
using Fivet.ZeroIce.model;
using Ice;

namespace Fivet.Server
{
    class TheSystemImpl : TheSystemDisp_
    {
        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTime.Now.Ticks - clientTime;
        }
    }

    class Program
    {
        private static readonly int PORT = 8080;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the Server...");
            
            using(var communicator = BuildCommunicator())
            {
                // The adapter: https://doc.zeroc.com/ice/3.7/client-side-features/proxies/proxy-and-endpoint-syntax
                // tpc (protocol) -z (compression) -t 15000 (imteout in ms) -p 8888 (port to bind)
                var theAdapter = communicator.createObjectAdapterWithEndpoints("TheAdapter","tcp -z -t 15000 -p "+ PORT);

                // Inline implementation (lambda)
                TheSystem theSystem = new TheSystemImpl();

                // Register TheSystem in the frameworks
                theAdapter.add(theSystem, Util.stringToIdentity("TheSystem"));

                // Everything ok, continue
                theAdapter.activate();

                // ... waiting
                Console.WriteLine("Waiting for connections...");
                communicator.waitForShutdown();
            }
            Console.WriteLine("Communication Ended.");
        }

        /// <summary>
        /// The Communicator
        /// <summary>
        private static Communicator BuildCommunicator()
        {
            // Console.WriteLine("[+] Building The Communicator...");

            // ZeroC properties
            Properties properties = Util.createProperties();

            properties.setProperty("Ice.Trace.Admin.Properties","1");
            properties.setProperty("Ice.Trace.Locator","2");
            properties.setProperty("Ice.Trace.Network","3");
            properties.setProperty("Ice.Trace.Protocol","4");
            properties.setProperty("Ice.Trace.Slicing","5");
            properties.setProperty("Ice.Trace.ThreadPool","6");
            properties.setProperty("Ice.Compression.Level","7");

            // The ZeroC framework
            InitializationData initializationData = new InitializationData();
            initializationData.properties = properties;

            var communicator = Ice.Util.initialize(initializationData);

            return communicator;
        }
    }
}
