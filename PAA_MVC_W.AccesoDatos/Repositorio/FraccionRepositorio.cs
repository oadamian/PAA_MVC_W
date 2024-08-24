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
    public class FraccionRepositorio : Repositorio<Fraccion>, IFraccionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public FraccionRepositorio(ApplicationDbContext db):base(db) 
        {
                _db = db;
        }


        public void Actualizar(Fraccion fraccion)
        {
            //antes de actualizar debo capturar el valor actual de mi objeto con lo siguiente
            var fraccionBD = _db.Fraccions.FirstOrDefault(b=>b.Id == fraccion.Id);

            if (fraccionBD != null)
            {
                fraccionBD.Bis= fraccion.Bis;
                fraccionBD.Frac= fraccion.Frac;
                fraccionBD.Descripcion= fraccion.Descripcion;
                fraccionBD.Estado= fraccion.Estado;
                fraccionBD.ArticuloId = fraccion.ArticuloId;
          
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosArticulos(string obj)
        {
            return _db.Articulos
           .Where(u => u.Estado == true)
           .Select(u => new SelectListItem
           {
               Text = u.Num.ToString(),
               Value = u.Id.ToString()
           }).ToList();
        }


        
    }
}

//de aqui se creara la unidad de trabajo como buena practica