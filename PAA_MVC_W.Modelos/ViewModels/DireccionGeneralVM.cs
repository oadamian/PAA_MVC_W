using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos.ViewModels
{
   public class DireccionGeneralVM
    {

        public DireccionGeneral DireccionGeneral { get; set; }

        public IEnumerable<SelectListItem> UnidadAuditoraLista { get; set; }
    }
}
