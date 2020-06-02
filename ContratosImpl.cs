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

        public override Ficha ingresarFicha (Ficha ficha, Current current = null)
        {
            throw new System.NotImplementedException();
        }
        public override Ficha obtenerFicha (int numeroFicha, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Create the Persona
        /// </summary>
        /// <param name="persona">to save</param>
        /// <param name="current">the context of zeroIce</param>
        /// <returns></returns>
        public override Persona ingresarDueno(Persona persona, Current current = null)
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

        public override Persona obtenerDueno (string rut, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override Control ingresarControl(int numeroFicha, Control control, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override Foto ingresarFoto (Foto foto, Current current = null)
        {
            throw new System.NotImplementedException();
        }

        public override Examen ingresarExamen (Examen examen, Current current = null)
        {
            throw new System.NotImplementedException();
        }
    }
}