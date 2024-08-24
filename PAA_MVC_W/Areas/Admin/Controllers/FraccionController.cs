using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PAA_MVC_W.AccesoDatos.Repositorio;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.Modelos.ViewModels;
using PAA_MVC_W.Utilidades;
//Paso 11 se crean los ocntroladores para las tablas 

namespace PAA_MVC_W.Areas.Admin.Controllers
{
    [Area("Admin")] //siempre se debe de poner a que area pertenece el controlador si no no va a correr
    public class FraccionController : Controller
    {


        private readonly IUnidadTrabajo _unidadTrabajo;// se instancia la interfaz de UnidadTrabajo

        public FraccionController(IUnidadTrabajo unidadTrabajo)//constructor
        {
            _unidadTrabajo = unidadTrabajo;// se inicializa
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            FraccionVM fraccionVM = new FraccionVM()
            {
                Fraccion = new Fraccion(),
                ArticuloLista= _unidadTrabajo.Fraccion.ObtenerTodosArticulos("Articulo"),
          
            };

            if (id == null)
            {
                //crear nuevo 

                fraccionVM.Fraccion.Estado = true;
                return View(fraccionVM);
            }
            else
            {

                fraccionVM.Fraccion = await _unidadTrabajo.Fraccion.Obtener(id.GetValueOrDefault());
                if(fraccionVM.Fraccion==null)
                {
                    return NotFound();
                }

                // Si la Unidad Auditora está seleccionada, habilita la lista de Dirección General
                if (fraccionVM.Fraccion.ArticuloId != 0)
                {


                    fraccionVM.ArticuloLista = _unidadTrabajo.Fraccion.ObtenerTodosArticulos("Articulo");

                }

                return View(fraccionVM);
            }

          
        }



        //Las regiones sirven para poner comentarios en este caso servira para separar los API's que seran llamados desde JS 
        #region API


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(FraccionVM fraccionVM)
        {
            if (ModelState.IsValid)
            {
                if (fraccionVM.Fraccion.Id == 0)
                {
                    await _unidadTrabajo.Fraccion.Agregar(fraccionVM.Fraccion);
                    TempData[DS.Exitosa] = "Fracción Creada Exitsamente";
                }
                else
                {
                    _unidadTrabajo.Fraccion.Actualizar(fraccionVM.Fraccion);
                    TempData[DS.Exitosa] = "Fracción Actualizada Exitsamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al insertar la Fracción";

            // Si el modelo no es válido, recargar las listas desplegables

            fraccionVM.ArticuloLista = _unidadTrabajo.Fraccion.ObtenerTodosArticulos("Articulo");
       
            return View(fraccionVM);
        }

        [HttpGet]//medoto API de tipo get "IactionResult no solo devuelve lista sino tambien objetos ocn formato Json " y la mandaremos a llamar desde el js
        public async Task<IActionResult> ObtenerTodos()
        {

            //accedemos a la unidad de trabajo y traemos la funcion obtener todos 
            var todos = await _unidadTrabajo.Fraccion.ObtenerTodos(incluirPropiedades:"Articulo"); 
            //aqui retornamos un Json y se esta renombrando la variable a "data" y asi es como se mandara a llamar desde JS
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var fraccionBD = await _unidadTrabajo.Fraccion.Obtener(id);
            if(fraccionBD == null)
            {
                return Json(new {success= false,message="Error al borrar la Fracción"});

            }
            _unidadTrabajo.Fraccion.Remover(fraccionBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Fracción borrada exitosamente" });
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
