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
    public class UnidadAuditoraConfiguracion : IEntityTypeConfiguration<UnidadAuditora>
    {
        public void Configure(EntityTypeBuilder<UnidadAuditora> builder) 
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.Cc).IsRequired();
            builder.Property(x=>x.Siglas).IsRequired().HasMaxLength(60);
            builder.Property(x=>x.Nombre_UA).IsRequired().HasMaxLength(120);
            builder.Property(x=>x.Estado).IsRequired();
        }
    }
}
