using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;

using System.Threading.Tasks;

namespace PAA_MVC_W.Filters
{
    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _role;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CustomAuthorizeFilter(string role, IUnidadTrabajo unidadTrabajo)
        {
            _role = role;
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Eliminar el prefijo "ASF\\" si está presente
            user = user.Replace("ASF\\", "");

            // Verificar el rol del usuario en la base de datos
            var usuario = await _unidadTrabajo.Usuario.ObtenerPrimero(u => u.UserName == user);
            var rol = await _unidadTrabajo.Rol.Obtener(usuario.RolId.Value);

            if (usuario == null || rol.Nombre != _role)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Si el usuario tiene el rol requerido, continuar
        }
    }
}
