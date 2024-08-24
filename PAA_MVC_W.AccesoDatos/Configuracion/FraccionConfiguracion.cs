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
    public class FraccionConfiguracion : IEntityTypeConfiguration<Fraccion>
    {
        public void Configure(EntityTypeBuilder<Fraccion> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Frac).IsRequired();
            builder.Property(x => x.Bis).HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.ArticuloId).IsRequired();
  

            /*relaciones*/
            builder.HasOne(x => x.Articulo).WithMany()
                .HasForeignKey(x => x.ArticuloId)
                .OnDelete(DeleteBehavior.NoAction);//el on delete es para no jorobar la eliminacon en cascada
        }
    }
}
