using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class DireccionGeneral
    {
        [Key]//esto es un dataannotations y serve para lso tipos de columnas y si son requeridos o no etc
        public int Id { get; set; }

        [Required(ErrorMessage = "Centro de Costos es requerido")]
        public int Cc { get; set; }

        [Required(ErrorMessage = "Siglas es requerido")]
        [MaxLength(100, ErrorMessage = "Siglas no mayor a 100 caracteres")]
        public string Siglas { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(240, ErrorMessage = "Nombre no mayor a 240 caracteres")]
        public string Nombre_DG { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }



        //intersepcion
        [Required(ErrorMessage = "Unidad Auditora es requerido")]
        public int UnidadAuditoraId { get; set; }

        //navegacion 
        [ForeignKey("UnidadAuditoraId")]
        public UnidadAuditora UnidadAuditora { get; set; }




    }
}
