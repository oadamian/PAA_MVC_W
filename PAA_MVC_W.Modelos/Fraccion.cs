using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class Fraccion
    {

        [Key]//esto es un dataannotations y serve para lso tipos de columnas y si son requeridos o no etc
        public int Id { get; set; }

        [Required(ErrorMessage = "Fraccion es requerido")]
        [MaxLength(100,ErrorMessage ="Fraccion no mayor a 60")]
        public string Frac { get; set; }

        [MaxLength(60, ErrorMessage = "Numeral no mayor a 60 caracteres")]
        public string Bis { get; set; }

        [Required(ErrorMessage = "Descripcion es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

        //intersepcion
        [Required(ErrorMessage = "Articulo es requerido")]
        public int ArticuloId { get; set; }

        //navegacion 
        [ForeignKey("ArticuloId")]
        public Articulo Articulo { get; set; }

    }
}
