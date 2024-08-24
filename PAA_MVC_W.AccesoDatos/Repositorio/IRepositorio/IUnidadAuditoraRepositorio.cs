using PAA_MVC_W.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Paso 6 Se crean interfaces individuales para cada tabla y eredan el modelo de su tabla

namespace PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadAuditoraRepositorio : IRepositorio<UnidadAuditora>
    {
        //se pone el metodo actualizar por cada uno de los repositorios individuales por tener su propias caracteristicas

        void Actualizar(UnidadAuditora unidadAuditora);
        Task ObtenerPrimero(Func<object, bool> value);
    }
}
