using PAA_MVC_W.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        void Actualizar(Usuario usuario);

        IEnumerable<SelectListItem> ObtenerTodasUnidadesAuditoras(string obj);

        IEnumerable<SelectListItem> ObtenerDireccionesGeneralesPorUnidadAuditora(int unidadAuditoraId);

        IEnumerable<SelectListItem> ObtenerTodosRoles(string obj);
    }
}