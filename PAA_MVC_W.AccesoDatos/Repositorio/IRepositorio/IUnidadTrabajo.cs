using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Paso 8 se crea intefas de unidad de trabajo 
namespace PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo: IDisposable //el idisposable permite desaserte recurso que ayas obtenido del sistema y objetos inecesarios  
    {

        IUnidadAuditoraRepositorio UnidadAuditora {  get; }

        IDireccionGeneralRepositorio DireccionGeneral { get; }

        IArticuloRepositorio Articulo { get; }   

        IFraccionRepositorio Fraccion {  get; }

        IActividadRepositorio Actividad { get; }

        IUsuarioRepositorio Usuario { get; }

        IRolRepositorio Rol { get; }


        Task Guardar();
    }
}
