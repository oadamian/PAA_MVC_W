using Microsoft.EntityFrameworkCore;
using PAA_MVC_W.AccesoDatos.Data;
using PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


//Paso 5 Se implementan las interfaces en este caso la interfaz repositorio generico 
namespace PAA_MVC_W.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;//se crea para traerla desde Data/ApplicationDbContext
        internal DbSet<T> dbSet; //generica

        public Repositorio(ApplicationDbContext db)//contructor se crea con 'ctor + tecla tab'
        {
            _db = db;//inicializa
            this.dbSet=_db.Set<T>();//se setea
        }

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); // equivale a un insert into en intity framework
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); //este dind assink solo trabaja con el id pero solo devuelve un valor Entity framewordk
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null) 
            {
                query = query.Where(filtro);// equivalente a select / select* from  where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(incluirProp);// este sirve para traer todas las relaciones que se tengan con la tabla y sus datos
                }
            }
            if(orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public  async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);// equivalente a select / select* from  where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);// este sirve para traer todas las relaciones que se tengan con la tabla y sus datos
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad); 
        }
    }
}
