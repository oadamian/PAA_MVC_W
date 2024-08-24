using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PAA_MVC_W.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.AccesoDatos.Configuracion
{
    public class ActividadConfiguracion : IEntityTypeConfiguration<Actividad>
    {
        public void Configure(EntityTypeBuilder<Actividad> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Clave).HasMaxLength(50);  // Configurar longitud máxima, si es necesario
            builder.Property(a => a.NombreActividad).IsRequired();  // Configurar como requerido y longitud máxima
            builder.Property(a => a.ObjetivoActividad).IsRequired();  // Configurar como requerido y longitud máxima
            builder.Property(a => a.CantidadProducto).IsRequired();
            builder.Property(a => a.TipoProducto).IsRequired().HasMaxLength(60);
            builder.Property(a => a.MedioVerificacionProducto).IsRequired();
            builder.Property(a => a.Fecha1);
            builder.Property(a => a.Fecha2);
            builder.Property(a => a.Fecha3);
            builder.Property(a => a.Fecha4);
            builder.Property(a => a.ObjetivoClave).IsRequired();
            builder.Property(a => a.ActividadControl).IsRequired();
            builder.Property(a => a.Supuestos).IsRequired();  // Configurar como requerido y longitud máxima
            builder.Property(a => a.Acciones).IsRequired();  // Configurar como requerido y longitud máxima
            builder.Property(a => a.AplicaIndicador).IsRequired();
            builder.Property(a => a.Estado).IsRequired();

            builder.Property(a => a.RowVersion).IsRowVersion();

            builder.HasOne(a => a.UnidadAuditora)
                   .WithMany()
                   .HasForeignKey(a => a.UnidadAuditoraId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.DireccionGeneral)
                   .WithMany()
                   .HasForeignKey(a => a.DireccionGeneralId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Cualquier otra configuración específica para la entidad
        }
    }
}
