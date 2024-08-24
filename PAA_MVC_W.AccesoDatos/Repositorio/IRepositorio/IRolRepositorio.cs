using PAA_MVC_W.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRolRepositorio : IRepositorio<Rol>
    {
        void Actualizar(Rol rol);
    }
}