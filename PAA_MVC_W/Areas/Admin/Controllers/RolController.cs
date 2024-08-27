using Microsoft.AspNetCore.Mvc;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Utilidades;
using Microsoft.AspNetCore.Authorization;
//paso 7
//Se crean los controles para cada uno de los modulos 
namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RolController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public RolController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)//puede o no recibir un id null por eso se pone int?
        {
            Rol rol = new Rol();
            if (id == null)
            {
                //crear una nueva categoria 
                rol.Estado = true;
                return View(rol);
            }
            //Actualizamos categoria
            rol = await _unidadTrabajo.Rol.Obtener(id.GetValueOrDefault());

            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        //¿Qué es CSRF?
        //CSRF es un ataque que ocurre cuando un usuario autenticado realiza una solicitud no intencionada a una aplicación web, que es aceptada y ejecutada por la aplicación porque la solicitud parece legítima.Esto puede llevar a acciones no autorizadas, como cambiar contraseñas, realizar transacciones o modificar datos.

        //¿Cómo funciona ValidateAntiForgeryToken?
        //En ASP.NET, ValidateAntiForgeryToken se utiliza junto con Html.AntiForgeryToken() para proteger formularios contra CSRF. 

        [HttpPost]
        [ValidateAntiForgeryToken]//evitar las falsificaciones de solicitudes de un sitio 
        public async Task<IActionResult> Upsert(Rol rol)
        {
            if (ModelState.IsValid)
            {
                if (rol.Id == 0)
                {
                    await _unidadTrabajo.Rol.Agregar(rol);
                    //se llama a las notificaciones rapidas
                    TempData[DS.Exitosa] = "Rol Creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Rol.Actualizar(rol);
                    //se llama a las notificaciones rapidas
                    TempData[DS.Exitosa] = "Rol Actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();

                return RedirectToAction(nameof(Index));
            }

            //se llama a las notificaciones rapidas
            TempData[DS.Error] = "Error al grabar el Rol";

            return View(rol);

        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()//no solo retorna un alista o vista si no tambien objetos ocn formato json
        {
            var todos = await _unidadTrabajo.Rol.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var catgoriaDB = await _unidadTrabajo.Rol.Obtener(id);
            if (catgoriaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el Rol" });
            }
            _unidadTrabajo.Rol.Remover(catgoriaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Rol borrado exitosamente" });
        }
        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Rol.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
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
