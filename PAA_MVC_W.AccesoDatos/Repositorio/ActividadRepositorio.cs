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
    public class ActividadRepositorio : Repositorio<Actividad>, IActividadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ActividadRepositorio(ApplicationDbContext db):base(db) 
        {
                _db = db;
        }


        public void Actualizar(Actividad actividad)
        {
            //antes de actualizar debo capturar el valor actual de mi objeto con lo siguiente
            var actividadBD = _db.Actividades.FirstOrDefault(b=>b.Id == actividad.Id);

            if (actividadBD != null)
            {
                // Actualizar los campos del modelo
                actividadBD.Clave = actividad.Clave;
                actividadBD.NombreActividad = actividad.NombreActividad;
                actividadBD.ObjetivoActividad = actividad.ObjetivoActividad;
                actividadBD.CantidadProducto = actividad.CantidadProducto;
                actividadBD.TipoProducto = actividad.TipoProducto;
                actividadBD.MedioVerificacionProducto = actividad.MedioVerificacionProducto;

                // Actualizar las fechas
                actividadBD.Fecha1 = actividad.Fecha1;
                actividadBD.Fecha2 = actividad.Fecha2;
                actividadBD.Fecha3 = actividad.Fecha3;
                actividadBD.Fecha4 = actividad.Fecha4;

                // Actualizar otros campos booleanos y de texto
                actividadBD.ObjetivoClave = actividad.ObjetivoClave;
                actividadBD.ActividadControl = actividad.ActividadControl;
                actividadBD.Supuestos = actividad.Supuestos;
                actividadBD.Acciones = actividad.Acciones;
                actividadBD.Especificaciones = actividad.Especificaciones;
                actividadBD.AplicaIndicador = actividad.AplicaIndicador;
                actividadBD.Estado = actividad.Estado;
                actividadBD.RowVersion = actividad.RowVersion;

                // Actualizar las relaciones con otras entidades
                actividadBD.UnidadAuditoraId = actividad.UnidadAuditoraId;
                actividadBD.DireccionGeneralId = actividad.DireccionGeneralId;

                // Guardar los cambios en la base de datos
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

  
     
    }
}

//de aqui se creara la unidad de trabajo como buena practica