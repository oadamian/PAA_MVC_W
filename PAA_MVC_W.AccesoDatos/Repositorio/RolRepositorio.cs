using PAA_MVC_W.AccesoDatos.Data;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using PAA_MVC_W.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.AccesoDatos.Repositorio
{
    public class RolRepositorio : Repositorio<Rol>, IRolRepositorio
    {
        private readonly ApplicationDbContext _db;

        public RolRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Rol rol)
        {
            var rolDb = _db.Rols.FirstOrDefault(r => r.Id == rol.Id);
            if (rolDb != null)
            {
                rolDb.Nombre = rol.Nombre;
                rolDb.Descripcion = rol.Descripcion;
                rolDb.Estado = rol.Estado;  
                _db.Rols.Update(rolDb);
            }
        }
    }
}