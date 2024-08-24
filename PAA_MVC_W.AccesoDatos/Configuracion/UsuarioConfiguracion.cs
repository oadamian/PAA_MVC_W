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
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Nombres).IsRequired().HasMaxLength(150);
            builder.Property(u => u.Apellidos).IsRequired().HasMaxLength(160);
            builder.Property(u => u.Estado).IsRequired();
            builder.Property(x => x.UnidadAuditoraId).IsRequired();
            builder.Property(x => x.DireccionGeneralId).IsRequired();
            builder.Property(x => x.RolId).IsRequired();

            builder.HasOne(u => u.UnidadAuditora)
                   .WithMany()
                   .HasForeignKey(u => u.UnidadAuditoraId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.DireccionGeneral)
                   .WithMany()
                   .HasForeignKey(u => u.DireccionGeneralId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Rol)
                .WithMany()
                .HasForeignKey(x => x.RolId)
                .OnDelete(DeleteBehavior.NoAction);//el on delete es para no jorobar la eliminacon en cascada
            // Cualquier otra configuración específica para la entidad
        }
    }
}
