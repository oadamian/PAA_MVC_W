using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Filters;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.Modelos.ViewModels;
using PAA_MVC_W.Utilidades;
//Paso 11 se crean los ocntroladores para las tablas 

namespace PAA_MVC_W.Areas.Admin.Controllers
{
    [Area("Admin")] //siempre se debe de poner a que area pertenece el controlador si no no va a correr
    [TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { new string[] { "Administrador" } })]
    public class DireccionGeneralController : Controller
    {


        private readonly IUnidadTrabajo _unidadTrabajo;// se instancia la interfaz de UnidadTrabajo

        public DireccionGeneralController(IUnidadTrabajo unidadTrabajo)//constructor
        {
            _unidadTrabajo = unidadTrabajo;// se inicializa
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            DireccionGeneralVM direccionGeneralVM =new DireccionGeneralVM()
            {
                DireccionGeneral =new DireccionGeneral(),
                UnidadAuditoraLista=_unidadTrabajo.DireccionGeneral.ObtenerTodosDropdownLista("UnidadAuditora")
            };

            if (id == null)
            {
                //crear nuevo 
                direccionGeneralVM.DireccionGeneral.Estado = true;
                return View(direccionGeneralVM);
            }
            else
            {
                direccionGeneralVM.DireccionGeneral = await _unidadTrabajo.DireccionGeneral.Obtener(id.GetValueOrDefault());
                if(direccionGeneralVM.DireccionGeneral==null)
                {
                    return NotFound();
                }
                return View(direccionGeneralVM);
            }

          
        }






        //Las regiones sirven para poner comentarios en este caso servira para separar los API's que seran llamados desde JS 
        #region API


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(DireccionGeneralVM direccionGeneralVM)
        {
            if (ModelState.IsValid)
            {
                if (direccionGeneralVM.DireccionGeneral.Id == 0)
                {
                    await _unidadTrabajo.DireccionGeneral.Agregar(direccionGeneralVM.DireccionGeneral);
                    TempData[DS.Exitosa] = "Área Creada Exitsamente";
                }
                else
                {
                    _unidadTrabajo.DireccionGeneral.Actualizar(direccionGeneralVM.DireccionGeneral);
                    TempData[DS.Exitosa] = "Área Actualizada Exitsamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al insertar la Dirección General";
            return View(direccionGeneralVM);
        }

        [HttpGet]//medoto API de tipo get "IactionResult no solo devuelve lista sino tambien objetos ocn formato Json " y la mandaremos a llamar desde el js
        public async Task<IActionResult> ObtenerTodos()
        {

            //accedemos a la unidad de trabajo y traemos la funcion obtener todos 
            var todos = await _unidadTrabajo.DireccionGeneral.ObtenerTodos(incluirPropiedades:"UnidadAuditora"); 
            //aqui retornamos un Json y se esta renombrando la variable a "data" y asi es como se mandara a llamar desde JS
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var direccionGeneralBD = await _unidadTrabajo.DireccionGeneral.Obtener(id);
            if(direccionGeneralBD == null)
            {
                return Json(new {success= false,message="Error al borrar la Dirección General"});

            }
            _unidadTrabajo.DireccionGeneral.Remover(direccionGeneralBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Dirección General borrada exitosamente" });
        }


        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre (string nombre, int id=0)
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
