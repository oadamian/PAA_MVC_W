using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class DireccionGeneralRepositorio : Repositorio<DireccionGeneral>, IDireccionGeneralRepositorio
    {
        private readonly ApplicationDbContext _db;

        public DireccionGeneralRepositorio(ApplicationDbContext db):base(db) 
        {
                _db = db;
        }


        public void Actualizar(DireccionGeneral direccionGeneral)
        {
            //antes de actualizar debo capturar el valor actual de mi objeto con lo siguiente
            var direccionGeneralBD = _db.DireccionGenerals.FirstOrDefault(b=>b.Id == direccionGeneral.Id);

            if (direccionGeneralBD != null)
            {
                direccionGeneralBD.Cc=direccionGeneral.Cc;
                direccionGeneralBD.Siglas= direccionGeneral.Siglas;
                direccionGeneralBD.Nombre_DG= direccionGeneral.Nombre_DG;
                direccionGeneralBD.Estado= direccionGeneral.Estado;
                direccionGeneralBD.UnidadAuditoraId = direccionGeneral.UnidadAuditoraId;   
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
            if(obj=="UnidadAuditora")
            {
                return _db.UnidadAuditoras.Where(c => c.Estado == true).Select(c => new SelectListItem 
                { 

                    Text=c.Nombre_UA,
                    Value=c.Id.ToString()

                });
                
            }
            return null;
        }
    }
}

//de aqui se creara la unidad de trabajo como buena practica