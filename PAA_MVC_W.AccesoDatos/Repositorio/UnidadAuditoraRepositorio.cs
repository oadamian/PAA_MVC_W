using PAA_MVC_W.AccesoDatos.Data;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Paso 7 se crea la clase para implementar la intefaz individual de cada tabla 

namespace PAA_MVC_W.AccesoDatos.Repositorio
{
    public class UnidadAuditoraRepositorio : Repositorio<UnidadAuditora>, IUnidadAuditoraRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UnidadAuditoraRepositorio(ApplicationDbContext db):base(db) 
        {
                _db = db;
        }


        public void Actualizar(UnidadAuditora unidadAuditora)
        {
            //antes de actualizar debo capturar el valor actual de mi objeto con lo siguiente
            var unidadAuditoraBD = _db.UnidadAuditoras.FirstOrDefault(b=>b.Id == unidadAuditora.Id);

            if (unidadAuditoraBD != null)
            {
                unidadAuditoraBD.Cc=unidadAuditora.Cc;
                unidadAuditoraBD.Siglas=unidadAuditora.Siglas;
                unidadAuditoraBD.Nombre_UA=unidadAuditora.Nombre_UA;
                unidadAuditoraBD.Estado=unidadAuditora.Estado;  
                _db.SaveChanges();
            }
        }

        public Task ObtenerPrimero(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}

//de aqui se creara la unidad de trabajo como buena practica