using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class ArticuloRepositorio : Repositorio<Articulo>, IArticuloRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepositorio(ApplicationDbContext db):base(db) 
        {
                _db = db;
        }


        public void Actualizar(Articulo articulo)
        {
            //antes de actualizar debo capturar el valor actual de mi objeto con lo siguiente
            var articuloBD = _db.Articulos.FirstOrDefault(b=>b.Id == articulo.Id);

            if (articuloBD != null)
            {
                articuloBD.Num= articulo.Num;
                articuloBD.Bis= articulo.Bis;
                articuloBD.Descripcion= articulo.Descripcion;
                articuloBD.Estado= articulo.Estado;
                articuloBD.UnidadAuditoraId = articulo.UnidadAuditoraId;
                articuloBD.DireccionGeneralId = articulo.DireccionGeneralId;
                _db.SaveChanges();
            }
        }

      

        public IEnumerable<SelectListItem> ObtenerTodasUnidadesAuditoras(string obj)
        {
            return _db.UnidadAuditoras
            .Where(u => u.Estado == true)
            .Select(u => new SelectListItem
            {
                Text = u.Nombre_UA,
                Value = u.Id.ToString()
            }).ToList();
        }


        public IEnumerable<SelectListItem> ObtenerDireccionesGeneralesPorUnidadAuditora(int unidadAuditoraId)
        {
            return _db.DireccionGenerals
            .Where(d => d.UnidadAuditoraId == unidadAuditoraId && d.Estado == true)
            .Select(d => new SelectListItem
            {
                Text = d.Nombre_DG,
                Value = d.Id.ToString()
            }).ToList();
        }

  
        //public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        //{
        //    if(obj=="UnidadAuditora")
        //    {
        //        return _db.UnidadAuditoras.Where(c => c.Estado == true).Select(c => new SelectListItem 
        //        { 

        //            Text=c.Nombre_UA,
        //            Value=c.Id.ToString()

        //        });

        //    }

        //    if (obj == "DireccionGeneral")
        //    {
        //        return _db.DireccionGenerals.Where(c => c.Estado == true).Select(c => new SelectListItem
        //        {

        //            Text = c.Nombre_DG,
        //            Value = c.Id.ToString()

        //        });

        //    }
        //    return null;
        //}
    }
}

//de aqui se creara la unidad de trabajo como buena practica