using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class Articulo
    {
        [Key]//esto es un dataannotations y serve para lso tipos de columnas y si son requeridos o no etc
        public int Id { get; set; }

        [Required(ErrorMessage = "Numero es requerido")]
        public int Num { get; set; }

       
        [MaxLength(60, ErrorMessage = "Numeral no mayor a 60 caracteres")]
        public string Bis { get; set; }

        [Required(ErrorMessage = "Descripcion es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }



        //intersepcion
        [Required(ErrorMessage = "Unidad Auditora es requerido")]
        public int UnidadAuditoraId { get; set; }

        //navegacion 
        [ForeignKey("UnidadAuditoraId")]
        public UnidadAuditora UnidadAuditora { get; set; }

        //intersepcion
        public int? DireccionGeneralId { get; set; }

        //navegacion 
        [ForeignKey("DireccionGeneralId")]
        public DireccionGeneral DireccionGeneral { get; set; }


    }
}
