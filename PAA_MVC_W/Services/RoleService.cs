// /Services/RoleService.cs
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;


public interface IRoleService
{
    Task<bool> UserHasAnyRoleAsync(string userName, params string[] roles);
}

public class RoleService : IRoleService
{
    private readonly IUnidadTrabajo _unidadTrabajo;

    public RoleService(IUnidadTrabajo unidadTrabajo)
    {
        _unidadTrabajo = unidadTrabajo;
    }

    public async Task<bool> UserHasAnyRoleAsync(string userName, params string[] roles)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        userName = userName.Replace("ASF\\", "");

        var usuario = await _unidadTrabajo.Usuario.ObtenerPrimero(u => u.UserName == userName);
        if (usuario == null)
            return false;

        var rol = await _unidadTrabajo.Rol.Obtener(usuario.RolId.Value);

        return roles.Contains(rol.Nombre);
    }
}