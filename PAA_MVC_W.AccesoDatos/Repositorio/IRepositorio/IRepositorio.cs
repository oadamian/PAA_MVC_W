using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//Paso 4 se crea la interfaz generica de Repositorio y se pone generico por que trabaja con cualquier
//objeto recibira cualquier que le mande UnidadAuditora etc 
namespace PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
       Task<T> Obtener(int id);

        //este metodo es asincrono
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro=null,//expresion va a funcionar como filtro
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,//para ordenarlos 
            string incluirPropiedades=null,//este se encarga de hacer los enlaces con otros objetos "tablas"
            bool isTracking=true//este sirve para acceder y modificar como un semaforo
            );

        //este metodo es asincrono
        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,//expresion va a funcionar como filtro
            string incluirPropiedades = null,//este se encarga de hacer los enlaces con otros objetos "tablas"
            bool isTracking = true//este sirve para acceder y modificar como un semaforo
            );

        //este metodo es asincrono
        Task Agregar(T entidad);// para agregar el objeto que estemos recibiendo a la base


        //los metodos de remover no pueden ser asincronos
        void Remover(T entidad);//para remover de la base

        void RemoverRango(IEnumerable<T> entidad);//para remover varios

        //como siguiente paso es inplementar la interfaz

    }
}
