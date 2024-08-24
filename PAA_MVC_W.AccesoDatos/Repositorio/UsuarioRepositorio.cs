using PAA_MVC_W.AccesoDatos.Data;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.AccesoDatos.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Usuario usuario)
        {
            var usuarioDb = _db.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (usuarioDb != null)
            {
                usuarioDb.UserName = usuario.UserName;
                usuarioDb.Email = usuario.Email;
                usuarioDb.Nombres = usuario.Nombres;
                usuarioDb.Apellidos = usuario.Apellidos;
                usuarioDb.Estado = usuario.Estado;
                usuarioDb.UnidadAuditoraId = usuario.UnidadAuditoraId;
                usuarioDb.DireccionGeneralId = usuario.DireccionGeneralId;

                _db.Usuarios.Update(usuarioDb);
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodasUnidadesAuditoras(string obj)
        {
            return _db.UnidadAuditoras.Select(u => new SelectListItem
            {
                Text = u.Nombre_UA, // Ajusta según la propiedad que represente el nombre de la unidad auditora.
                Value = u.Id.ToString()
            });
        }

        public IEnumerable<SelectListItem> ObtenerDireccionesGeneralesPorUnidadAuditora(int unidadAuditoraId)
        {
            return _db.DireccionGenerals
                .Where(d => d.UnidadAuditoraId == unidadAuditoraId)
                .Select(d => new SelectListItem
                {
                    Text = d.Nombre_DG, // Ajusta según la propiedad que represente el nombre de la dirección general.
                    Value = d.Id.ToString()
                });
        }

        public IEnumerable<SelectListItem> ObtenerTodosRoles(string obj)
        {
            return _db.Rols.Select(r => new SelectListItem
            {
                Text = r.Nombre, // Ajusta según la propiedad que represente el nombre del rol.
                Value = r.Id.ToString()
            });
        }
    }
}