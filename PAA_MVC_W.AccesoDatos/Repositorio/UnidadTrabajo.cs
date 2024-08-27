using PAA_MVC_W.AccesoDatos.Data;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Paso 9 se implementa la interfaz unidad de trabajo

namespace PAA_MVC_W.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public IUnidadAuditoraRepositorio UnidadAuditora {  get; private set; }

        public IDireccionGeneralRepositorio DireccionGeneral { get; private set; }

        public IArticuloRepositorio Articulo { get; private set; }

        public IFraccionRepositorio Fraccion { get; private set; }
        public IActividadRepositorio Actividad { get; private set; }

        public IUsuarioRepositorio Usuario { get; private set; }
        public IRolRepositorio Rol { get; private set; }


        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db=db;
            UnidadAuditora=new UnidadAuditoraRepositorio(_db);
            DireccionGeneral=new DireccionGeneralRepositorio(_db);
            Articulo=new ArticuloRepositorio(_db);
            Fraccion=new FraccionRepositorio(_db);
            Actividad = new ActividadRepositorio(_db);
            Usuario=new UsuarioRepositorio(_db);
            Rol=new RolRepositorio(_db);
           
            
        }

        public void Dispose()//elimina todo lo que tenemos en memora y no se este usando
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
        //de aqui no svamos a agragr a unidad de trabajo como un servicio en program.cs
    }
}
