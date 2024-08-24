using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAA_MVC_W.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//paso 3 se crean las clases de configuracion "nombre_clase" seguido de "Configuracion" como buena 
//practica aqui es fluen API y hasta aqui se uso tambien codefirst de aqui se van a crear el
//patron repositorio para que se puedan consultar agregas o remover * Esto sera en
//PAA_MVC_W.AccesoDatos se crean carpetas Repositorio y en ella se crea
//otra carpeta IRepositorio LA cual contendra las Interfaces


namespace PAA_MVC_W.AccesoDatos.Configuracion
{
    public class DireccionGeneralConfiguracion : IEntityTypeConfiguration<DireccionGeneral>
    {
        public void Configure(EntityTypeBuilder<DireccionGeneral> builder) 
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.Cc).IsRequired();
            builder.Property(x=>x.Siglas).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Nombre_DG).IsRequired().HasMaxLength(240);
            builder.Property(x=>x.Estado).IsRequired();
            builder.Property(x=>x.UnidadAuditoraId).IsRequired();

            /*relaciones*/
            builder.HasOne(x=>x.UnidadAuditora).WithMany()
                .HasForeignKey(x=>x.UnidadAuditoraId)
                .OnDelete(DeleteBehavior.NoAction);//el on delete es para no jorobar la eliminacon en cascada
        }
    }
}
