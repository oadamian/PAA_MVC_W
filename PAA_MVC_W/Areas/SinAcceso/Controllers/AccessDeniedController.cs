using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PAA_MVC_W.Areas.SinAcceso.Controllers
{

    [Area("SinAcceso")]
    [Authorize]
    public class AccessDeniedController : Controller
    {
        [AllowAnonymous] // Permitir acceso anónimo
        public IActionResult Index()
        {
            return View(); // Cargar la vista de acceso denegado
        }
    }
}
