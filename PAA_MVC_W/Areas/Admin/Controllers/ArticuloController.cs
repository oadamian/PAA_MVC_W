using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PAA_MVC_W.AccesoDatos.Repositorio;
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

    public class ArticuloController : Controller
    {


        private readonly IUnidadTrabajo _unidadTrabajo;// se instancia la interfaz de UnidadTrabajo

        public ArticuloController(IUnidadTrabajo unidadTrabajo)//constructor
        {
            _unidadTrabajo = unidadTrabajo;// se inicializa
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ArticuloVM articulolVM = new ArticuloVM()
            {
                Articulo = new Articulo(),
                UnidadAuditoraLista = _unidadTrabajo.Articulo.ObtenerTodasUnidadesAuditoras("UnidadAuditora"),
                DireccionGeneralLista = new List<SelectListItem>(),
                IsDireccionGeneralDisabled = true // Inicialmente deshabilitado
            };

            if (id == null)
            {
                //crear nuevo 
          
                articulolVM.Articulo.Estado = true;
                return View(articulolVM);
            }
            else
            {
               
                articulolVM.Articulo = await _unidadTrabajo.Articulo.Obtener(id.GetValueOrDefault());
                if(articulolVM.Articulo==null)
                {
                    return NotFound();
                }

                // Si la Unidad Auditora está seleccionada, habilita la lista de Dirección General
                if (articulolVM.Articulo.UnidadAuditoraId != 0)
                {
                    articulolVM.IsDireccionGeneralDisabled = false;
                    // Llenar la lista de Direcciones Generales en función de la Unidad Auditora seleccionada
                    articulolVM.DireccionGeneralLista = _unidadTrabajo.Articulo.ObtenerDireccionesGeneralesPorUnidadAuditora(articulolVM.Articulo.UnidadAuditoraId);

                }

                return View(articulolVM);
            }

          
        }


        public JsonResult ObtenerDireccionesGenerales(int unidadAuditoraId)
        {
            var direcciones = _unidadTrabajo.Articulo.ObtenerDireccionesGeneralesPorUnidadAuditora(unidadAuditoraId);
            return Json(direcciones);
        }




        //Las regiones sirven para poner comentarios en este caso servira para separar los API's que seran llamados desde JS 
        #region API


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                if (articuloVM.Articulo.Id == 0)
                {
                    await _unidadTrabajo.Articulo.Agregar(articuloVM.Articulo);
                    TempData[DS.Exitosa] = "Articulo Creada Exitsamente";
                }
                else
                {
                    _unidadTrabajo.Articulo.Actualizar(articuloVM.Articulo);
                    TempData[DS.Exitosa] = "Articulo Actualizada Exitsamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al insertar el Articulo";

            // Si el modelo no es válido, recargar las listas desplegables
            articuloVM.UnidadAuditoraLista = _unidadTrabajo.Articulo.ObtenerTodasUnidadesAuditoras("UnidadAuditora");
            articuloVM.DireccionGeneralLista = _unidadTrabajo.Articulo.ObtenerDireccionesGeneralesPorUnidadAuditora(articuloVM.Articulo.UnidadAuditoraId);

            return View(articuloVM);
        }

        [HttpGet]//medoto API de tipo get "IactionResult no solo devuelve lista sino tambien objetos ocn formato Json " y la mandaremos a llamar desde el js
       


        public async Task<IActionResult> ObtenerTodos()
        {
            //accedemos a la unidad de trabajo y traemos la funcion obtener todos 
            var todos = await _unidadTrabajo.Articulo.ObtenerTodos(incluirPropiedades:"UnidadAuditora,DireccionGeneral"); 
            //aqui retornamos un Json y se esta renombrando la variable a "data" y asi es como se mandara a llamar desde JS
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var articuloBD = await _unidadTrabajo.Articulo.Obtener(id);
            if(articuloBD == null)
            {
                return Json(new {success= false,message="Error al borrar el Articulo"});

            }
            _unidadTrabajo.Articulo.Remover(articuloBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Articulo borrado exitosamente" });
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
