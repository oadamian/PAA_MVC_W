using Microsoft.EntityFrameworkCore;
using PAA_MVC_W.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PAA_MVC_W.AccesoDatos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        //paso 2 se crea una propiedad de tipo dbset y es de nuestro modelo de aqui nos vamos a trabajar
        //con fluent API 
        //el fluent API sirve para configurar clases de dominio que cambian las caracteristicas del model 
        //builder se puede configurar mas a fondo de las caracteristicas de la base 


        public DbSet<UnidadAuditora> UnidadAuditoras { get; set; }

        public DbSet<DireccionGeneral> DireccionGenerals { get; set; }

        public DbSet<Articulo> Articulos { get; set; }


        public DbSet<Fraccion> Fraccions { get; set; }

        public DbSet<Actividad> Actividades { get; set; } // Asegúrate de tener esta DbSet

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Rols { get; set; }

   

        //oberride o sobrecarga 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        //para la validacion de fechas
        public override int SaveChanges()
        {
            //ValidateActividades();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ValidateActividades();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ValidateActividades()
        {
            foreach (var entry in ChangeTracker.Entries<Actividad>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    if (!entry.Entity.IsValid())
                    {
                        throw new ValidationException("Al menos una de las fechas (Fecha1, Fecha2, Fecha3, Fecha4) es requerida.");
                    }
                }
            }
        }

        //despues se crea la carpeta para crear archivos de configuracion por cada modelo 
        //para la implementacion de fluent API en PAA_MVC_W.AccesoDatos se crea la carpeta llamada
        //Configuracion
    }
}
