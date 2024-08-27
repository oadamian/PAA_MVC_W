using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class Usuario
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "UserName es Requerido")]
        [MaxLength(50,ErrorMessage = "UserName es menor a 50 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Correo es Requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [MaxLength(150,ErrorMessage ="Nombres es menor a 150 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [MaxLength(160, ErrorMessage = "Apellidos es menor a 160 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set; }


        [Required(ErrorMessage = "Rol es Requerido")]
        public int? RolId { get; set; }
        public Rol Rol { get; set; }

        [Required(ErrorMessage = "Unidad Auditora es requerida")]
        public int UnidadAuditoraId { get; set; }
        public UnidadAuditora UnidadAuditora { get; set; }

        [Required]
        public int? DireccionGeneralId { get; set; }
        public DireccionGeneral DireccionGeneral { get; set; }

      

    }
}
