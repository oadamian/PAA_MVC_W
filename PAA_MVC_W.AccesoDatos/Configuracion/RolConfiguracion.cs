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
    public class RolConfiguracion : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(r => r.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Descripcion).IsRequired();

        }
    }
}
