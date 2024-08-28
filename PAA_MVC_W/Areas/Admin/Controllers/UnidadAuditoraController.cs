using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Filters;
using PAA_MVC_W.Modelos;
using PAA_MVC_W.Utilidades;
//Paso 11 se crean los ocntroladores para las tablas 

namespace PAA_MVC_W.Areas.Admin.Controllers
{
    [Area("Admin")] //siempre se debe de poner a que area pertenece el controlador si no no va a correr
    [TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { new string[] { "Administrador" } })]
    public class UnidadAuditoraController : Controller
    {


        private readonly IUnidadTrabajo _unidadTrabajo;// se instancia la interfaz de UnidadTrabajo

        public UnidadAuditoraController(IUnidadTrabajo unidadTrabajo)//constructor
        {
            _unidadTrabajo = unidadTrabajo;// se inicializa
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            UnidadAuditora unidadAuditora=new UnidadAuditora();

            if(id==null)
            {
                //crear nueva area
                unidadAuditora.Estado=true;
                return View(unidadAuditora);

            }
            //actualizar area
            unidadAuditora = await _unidadTrabajo.UnidadAuditora.Obtener(id.GetValueOrDefault());
            if(unidadAuditora==null)
            {
                return NotFound();
            }
            return View(unidadAuditora);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(UnidadAuditora unidadAuditora)
        {
            if(ModelState.IsValid)
            {
                if(unidadAuditora.Id == 0)
                {
                    await _unidadTrabajo.UnidadAuditora.Agregar(unidadAuditora);
                    TempData[DS.Exitosa] = "Área Creada Exitsamente";
                }
                else
                {
                    _unidadTrabajo.UnidadAuditora.Actualizar(unidadAuditora);
                    TempData[DS.Exitosa] = "Área Actualizada Exitsamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al insertar Área";
            return View(unidadAuditora);
        }


        //Las regiones sirven para poner comentarios en este caso servira para separar los API's que seran llamados desde JS 
        #region API


        [HttpGet]//medoto API de tipo get "IactionResult no solo devuelve lista sino tambien objetos ocn formato Json " y la mandaremos a llamar desde el js
        public async Task<IActionResult> ObtenerTodos()
        {

            //accedemos a la unidad de trabajo y traemos la funcion obtener todos 
            var todos = await _unidadTrabajo.UnidadAuditora.ObtenerTodos(); 
            //aqui retornamos un Json y se esta renombrando la variable a "data" y asi es como se mandara a llamar desde JS
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var unidadAuditoraBD = await _unidadTrabajo.UnidadAuditora.Obtener(id);
            if(unidadAuditoraBD==null)
            {
                return Json(new {success= false,message="Error al borrar el Área"});

            }
            _unidadTrabajo.UnidadAuditora.Remover(unidadAuditoraBD);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Área borrada exitpsamente" });
        }


        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre (string nombre, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.UnidadAuditora.ObtenerTodos();

           
                if (id == 0)
                {
                    valor = lista.Any(b => b.Nombre_UA.ToLower().Trim() == nombre.ToLower().Trim());
                }
                else
                {
                    valor = lista.Any(b => b.Nombre_UA.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
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
            var lista = await _unidadTrabajo.UnidadAuditora.ObtenerTodos();
           
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
