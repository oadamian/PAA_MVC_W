using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAA_MVC_W.AccesoDatos.Repositorio;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Filters;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.Modelos.ViewModels;
using PAA_MVC_W.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace PAA_MVC_W.Areas.Admin.Controllers
{
    [Area("Capturista")]
    //[TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { "Capturista_UA_Admin" })]
    [TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { new string[] { "Administrador","Capturista_UA_Admin", "Capturista_UA", "Capturista_DG" } })]
    public class ActividadController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ActividadController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ActividadVM actividadVM = new ActividadVM()
            {
                Actividad = new Actividad(),
                UnidadAuditoraLista = _unidadTrabajo.Actividad.ObtenerTodasUnidadesAuditoras("UnidadAuditora"),
                DireccionGeneralLista = new List<SelectListItem>(),
                IsDireccionGeneralDisabled = true // Inicialmente deshabilitado
            };

            if (id == null)
            {
                // Crear nuevo
                actividadVM.Actividad.Estado = true;
                return View(actividadVM);
            }
            else
            {
                // Editar
                actividadVM.Actividad = await _unidadTrabajo.Actividad.Obtener(id.GetValueOrDefault());
                if (actividadVM.Actividad == null)
                {
                    return NotFound();
                }

                // Si la Unidad Auditora está seleccionada, habilitar la lista de Dirección General
                if (actividadVM.Actividad.UnidadAuditoraId != 0)
                {
                    actividadVM.IsDireccionGeneralDisabled = false;
                    actividadVM.DireccionGeneralLista = _unidadTrabajo.Actividad.ObtenerDireccionesGeneralesPorUnidadAuditora(actividadVM.Actividad.UnidadAuditoraId);
                }

                return View(actividadVM);
            }
        }

        public JsonResult ObtenerDireccionesGenerales(int unidadAuditoraId)
        {
            var direcciones = _unidadTrabajo.Actividad.ObtenerDireccionesGeneralesPorUnidadAuditora(unidadAuditoraId);
            return Json(direcciones);
        }

        #region API

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ActividadVM actividadVM)
        {
            if (actividadVM.Actividad.ObjetivoClave == false)
            {
                ModelState.Remove("Actividad.Supuestos");
                ModelState.Remove("Actividad.Acciones");

                actividadVM.Actividad.Supuestos = "No aplica";
                actividadVM.Actividad.Acciones = "No aplica";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (actividadVM.Actividad.Id == 0)
                    {
                        actividadVM.Actividad.Clave = await GenerarClaveAsync(actividadVM.Actividad);
                        await _unidadTrabajo.Actividad.Agregar(actividadVM.Actividad);
                    }
                    else
                    {
                        actividadVM.Actividad.Clave = await ObtenerClaveAnteriorActualizar(actividadVM.Actividad);
                        _unidadTrabajo.Actividad.Actualizar(actividadVM.Actividad);
                    }

                    await _unidadTrabajo.Guardar();
                    TempData[DS.Exitosa] = actividadVM.Actividad.Id == 0 ? "Actividad Creada Exitosamente" : "Actividad Actualizada Exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var databaseEntity = await _unidadTrabajo.Actividad.Obtener(actividadVM.Actividad.Id);

                    if (databaseEntity == null)
                    {
                        TempData[DS.Error] = "La actividad ya no existe en la base de datos.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, "Hubo un conflicto de concurrencia. Los datos actuales han sido recargados.");
                    actividadVM.Actividad.RowVersion = databaseEntity.RowVersion;

                    // Re-establecer listas desplegables
                    actividadVM.UnidadAuditoraLista = _unidadTrabajo.Actividad.ObtenerTodasUnidadesAuditoras("UnidadAuditora");
                    actividadVM.DireccionGeneralLista = _unidadTrabajo.Actividad.ObtenerDireccionesGeneralesPorUnidadAuditora(actividadVM.Actividad.UnidadAuditoraId);

                    return View(actividadVM);
                }
                catch (Exception ex)
                {
                    TempData[DS.Error] = "Ocurrió un error al guardar los datos: " + ex.Message;
                }
            }
            else
            {
                // Si el modelo no es válido, recargar las listas desplegables
                actividadVM.UnidadAuditoraLista = _unidadTrabajo.Actividad.ObtenerTodasUnidadesAuditoras("UnidadAuditora");
                actividadVM.DireccionGeneralLista = _unidadTrabajo.Actividad.ObtenerDireccionesGeneralesPorUnidadAuditora(actividadVM.Actividad.UnidadAuditoraId);
            }
            return View(actividadVM);

        }


        //----------------------------------------------------------------- Se genera la clave antes de guardar cuando se crea la actividad --------------------------------------------------------------------
        private async Task<string> GenerarClaveAsync(Actividad actividad)
        {
            // Obtener todas las actividades relacionadas con la misma UnidadAuditoraId
            var actividadesRelacionadas = await _unidadTrabajo.Actividad.ObtenerTodos(
                a => a.UnidadAuditoraId == actividad.UnidadAuditoraId && actividad.Estado==true);

            // Contar cuántas actividades hay relacionadas
            int numeroActividades = actividadesRelacionadas.Count() + 1; // Sumamos 1 para el nuevo registro

            // Obtener información de la UnidadAuditora relacionada
            var unidadAuditora = await _unidadTrabajo.UnidadAuditora.Obtener(actividad.UnidadAuditoraId);

            // Generar el código basado en la cantidad de actividades y otros datos
            return $"{unidadAuditora.Siglas}-{numeroActividades}";
        }

        //------------------------------------------------------------------- Se obtiene la clave anterior antes de guardar  ------------------------------------------------------------------------------------

        private async Task<string> ObtenerClaveAnteriorActualizar(Actividad actividad)
        {
            // Obtener la clave anterior
            var ClaveAnteriorActividad = await _unidadTrabajo.Actividad.Obtener(actividad.Id);

            // Verificar si la clave es null y retornar un valor predeterminado si es necesario
            if (!string.IsNullOrEmpty(ClaveAnteriorActividad?.Clave))
            {
                return ClaveAnteriorActividad.Clave;
            }
            else
            {
                return "Sin clave";
            }
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Actividad.ObtenerTodos(incluirPropiedades: "UnidadAuditora,DireccionGeneral");
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, byte[] rowVersion)
        {
            var actividadBD = await _unidadTrabajo.Actividad.Obtener(id);
            if (actividadBD == null)
            {
                return Json(new { success = false, message = "Error al borrar la Actividad" });
            }

            try
            {
                // Verifica que la RowVersion coincida antes de eliminar
                if (!actividadBD.RowVersion.SequenceEqual(rowVersion))
                {
                    return Json(new { success = false, message = "La actividad ha sido modificada por otro usuario y no se puede eliminar." });
                }

                _unidadTrabajo.Actividad.Remover(actividadBD);
                await _unidadTrabajo.Guardar();
                return Json(new { success = true, message = "Actividad borrada exitosamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false, message = "Error de concurrencia al intentar eliminar la Actividad." });
            }
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.DireccionGeneral.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre_DG.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre_DG.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        [ActionName("ValidarSiglas")]
        public async Task<IActionResult> ValidarSiglas(string siglas, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.DireccionGeneral.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(c => c.Siglas.ToLower().Trim() == siglas.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(c => c.Siglas.ToLower().Trim() == siglas.ToLower().Trim() && c.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion
    }
}