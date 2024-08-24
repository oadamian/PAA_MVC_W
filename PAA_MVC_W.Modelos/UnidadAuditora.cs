using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// paso 1 se crea el modelo para las tablas despues de crear el modelo nos vamos 
// a Acceso datos en ApplicationDbcontext.cs a crear lo siguiente
namespace PAA_MVC_W.Modelos
{
    public class UnidadAuditora
    {
        [Key]//esto es un dataannotations y serve para lso tipos de columnas y si son requeridos o no etc
        public int Id { get; set; }

        [Required(ErrorMessage = "Centro de Costos es requerido")]
        public int Cc { get; set; }

        [Required(ErrorMessage ="Siglas es requerido")]
        [MaxLength(60,ErrorMessage ="Siglas no mayor a 60 caracteres")]
        public string Siglas { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(120, ErrorMessage = "Nombre no mayor a 120 caracteres")]
        public string Nombre_UA { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

    }
}
