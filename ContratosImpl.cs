using Fivet.Dao;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{
    /// <summary>
    /// The Implementation of the Contratos
    /// </summary>
    public class ContratosImpl : ContratosDisp_
    {
        /// <summary>
        /// The Logger
        /// </summary>
        private readonly ILogger<ContratosImpl> _logger;

        /// <summary>
        /// The Provider of DbContext
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        public ContratosImpl(ILogger<ContratosImpl> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _logger.LogDebug("Building the ContratosImpl...");
            _serviceScopeFactory = serviceScopeFactory;

            // Create the database
            _logger.LogInformation("Creating the Database...");
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Database.EnsureCreated();
                fc.SaveChanges();
            }
            _logger.LogDebug("Done.");
        }

        /// <summary>
        /// Create the Persona
        /// </summary>
        /// <param name="persona">to save</param>
        /// <param name="current">the context of zeroIce</param>
        /// <returns></returns>
        public override Persona crearPersona(Persona persona, Current current = null)
        {
            // Using the local scope
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Personas.Add(persona);
                fc.SaveChanges();
                return persona;
            }
        }
        
        public override Control crearControl(int NumeroFicha, Control control, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override Ficha crearFicha (Ficha ficha, Current current = null)
        {
            throw new System.NotImplementedException();
        }
    }
}