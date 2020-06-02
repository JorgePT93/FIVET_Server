using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;

namespace Fivet.Dao
{
    /// <summary>
    /// The Connection to the FivetDatabase
    /// </summary>
    public class FivetContext : DbContext
    {
        /// <summary>
        /// The Connection to the database
        /// </summary>
        public DbSet<Persona> Personas {get; set;}

        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Using SQLite
            optionsBuilder.UseSqLite("Data Source=fivet.db", options => 
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);                
            });
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Create the ER from Entity
        /// </summary>
        /// <param name="modelBuilder">to use</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Update the model
            modelBuilder.Entity<Persona>(p=>
            {
                // Primary key
                p.HasKey(p => p.uid);
                // Index in Email
                p.Property(p => p.email).IsRequired();
                p.HasIndex(p => p.email).IsUnique();
            });

            // Insert the data
            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    uid = 1,
                    nombre = "Jorge",
                    apellido = "Pizarro",
                    direccion = "Angamos #0618",
                    email = "jpt010@alumnos.ucn.cl"
                }
            );
        }
    }
}