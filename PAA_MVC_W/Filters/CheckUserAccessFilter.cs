// Filters/CheckUserAccessFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using System.Linq;
using System.Threading.Tasks;

namespace PAA_MVC_W.Filters
{
    public class CheckUserAccessFilter : IAsyncActionFilter
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ILogger<CheckUserAccessFilter> _logger;

        public CheckUserAccessFilter(IUnidadTrabajo unidadTrabajo, ILogger<CheckUserAccessFilter> logger)
        {
            _unidadTrabajo = unidadTrabajo;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Evitar bucle de redirección
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var controllerName = context.ActionDescriptor.RouteValues["controller"];

            if (controllerName == "AccessDenied" && actionName == "Index")
            {
                await next();
                return;
            }

            // Obtener el usuario de Windows
            var windowsUserName = context.HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(windowsUserName))
            {
                _logger.LogWarning("No se pudo obtener el nombre de usuario de Windows.");
                context.Result = new RedirectToActionResult("Index", "AccessDenied", new { area = "SinAcceso" });
                return;
            }

            // Eliminar el prefijo "ASF\\"
            windowsUserName = windowsUserName.Replace("ASF\\", "");

            // Verificar si el usuario tiene permiso
            var listaUsuarios = await _unidadTrabajo.Usuario.ObtenerTodos();
            bool hasAccess = listaUsuarios.Any(u => u.UserName.ToLower().Trim() == windowsUserName.ToLower().Trim());

            if (!hasAccess)
            {
                _logger.LogWarning($"El usuario {windowsUserName} no tiene permiso para acceder al sistema.");
                context.Result = new RedirectToActionResult("Index", "AccessDenied", new { area = "SinAcceso" });
                return;
            }

            // Continuar con la ejecución de la acción
            await next();
        }
    }
}