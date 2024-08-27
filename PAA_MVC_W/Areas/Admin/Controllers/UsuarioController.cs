using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.Modelos.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PAA_MVC_W.Utilidades;
using Microsoft.AspNetCore.Authorization;

namespace PAA_MVC_W.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UsuarioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public UsuarioController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            UsuarioVM usuarioVM = new()
            {
                Usuario = new Usuario(),
                UnidadAuditoraLista = _unidadTrabajo.Usuario.ObtenerTodasUnidadesAuditoras("UnidadAuditora"),
                DireccionGeneralLista = new List<SelectListItem>(),
                RolLista = _unidadTrabajo.Usuario.ObtenerTodosRoles("Rol"),
                IsDireccionGeneralDisabled = true // Inicialmente deshabilitado
            };

            if (id == null)
            {
                // Crear
                usuarioVM.Usuario.Estado = true;
                return View(usuarioVM);
            }
            else
            {
                // Editar
                usuarioVM.Usuario = await _unidadTrabajo.Usuario.Obtener(id.GetValueOrDefault());
                if (usuarioVM.Usuario == null)
                {
                    return NotFound();
                }

                // Si la Unidad Auditora está seleccionada, habilita la lista de Dirección General
                if (usuarioVM.Usuario.UnidadAuditoraId != 0)
                {
                    usuarioVM.IsDireccionGeneralDisabled = false;
                    // Llenar la lista de Direcciones Generales en función de la Unidad Auditora seleccionada
                    usuarioVM.DireccionGeneralLista = _unidadTrabajo.Articulo.ObtenerDireccionesGeneralesPorUnidadAuditora(usuarioVM.Usuario.UnidadAuditoraId);

                }

                return View(usuarioVM);
            }
        }

        public JsonResult ObtenerDireccionesGenerales(int unidadAuditoraId)
        {
            var direcciones = _unidadTrabajo.Articulo.ObtenerDireccionesGeneralesPorUnidadAuditora(unidadAuditoraId);
            return Json(direcciones);
        }




        #region API 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UsuarioVM usuarioVM)
        {
            if (ModelState.IsValid)
            {
                if (usuarioVM.Usuario.Id == 0)
                {
                    await _unidadTrabajo.Usuario.Agregar(usuarioVM.Usuario);
                    TempData[DS.Exitosa] = "Usuario Creado Exitsamente";
                }
                else
                {
                    _unidadTrabajo.Usuario.Actualizar(usuarioVM.Usuario);
                    TempData[DS.Exitosa] = "Usuario Actualizado Exitsamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al insertar el Usuario";

            // Si el modelo no es válido, recargar las listas desplegables
            usuarioVM.UnidadAuditoraLista = _unidadTrabajo.Usuario.ObtenerTodasUnidadesAuditoras("UnidadAuditora");
            usuarioVM.DireccionGeneralLista = _unidadTrabajo.Usuario.ObtenerDireccionesGeneralesPorUnidadAuditora(usuarioVM.Usuario.UnidadAuditoraId);
            usuarioVM.RolLista = _unidadTrabajo.Usuario.ObtenerTodosRoles("Rol");
            return View(usuarioVM);
        }



        [HttpGet]//medoto API de tipo get "IactionResult no solo devuelve lista sino tambien objetos ocn formato Json " y la mandaremos a llamar desde el js
        public async Task<IActionResult> ObtenerTodos()
        {

            //accedemos a la unidad de trabajo y traemos la funcion obtener todos 
            var todos = await _unidadTrabajo.Usuario.ObtenerTodos(incluirPropiedades: "UnidadAuditora,DireccionGeneral,Rol");
            //aqui retornamos un Json y se esta renombrando la variable a "data" y asi es como se mandara a llamar desde JS
            return Json(new { data = todos });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioDb = await _unidadTrabajo.Usuario.Obtener(id);
            if (usuarioDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar el usuario" });
            }

            _unidadTrabajo.Usuario.Remover(usuarioDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Usuario eliminado con éxito" });
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