using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos.ViewModels
{
   public class UsuarioVM
    {

        public Usuario Usuario { get; set; }

      

        public IEnumerable<SelectListItem> UnidadAuditoraLista { get; set; }

        public IEnumerable<SelectListItem> DireccionGeneralLista { get; set; }

        public IEnumerable<SelectListItem> RolLista { get; set; }

        // Nueva propiedad para manejar el estado de "disabled"
        public bool IsDireccionGeneralDisabled { get; set; } = true;  // Por defecto deshabilitado
    }
}
