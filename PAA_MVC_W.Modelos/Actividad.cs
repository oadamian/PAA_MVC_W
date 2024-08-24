using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class Actividad
    {

       

        [Key]//esto es un dataannotations y serve para lso tipos de columnas y si son requeridos o no etc
        public int Id { get; set; }

        //actividad
        public string Clave { get; set; }

        [Required(ErrorMessage = "Actividad es requerida")]
        public string NombreActividad { get; set; }

        [Required(ErrorMessage = "Actividad es requerida")]
        public string ObjetivoActividad{ get; set; }


        //producto
        [Required(ErrorMessage = "Cantidad es requerido")]
        public int CantidadProducto { get; set; }

        [Required(ErrorMessage = "Tipo Producto es requerido")]
        [MaxLength(60,ErrorMessage = "Tipo no mayor a 60 caracteres")]
        public string TipoProducto { get; set; }

        [Required(ErrorMessage = "Medio de Verificación es requerido")]
        public string MedioVerificacionProducto { get; set; }



        //Fechas

        [AtLeastOneRequired("Fecha1", "Fecha2", "Fecha3", "Fecha4", ErrorMessage = "Al menos una fecha es requerida")]

        public DateTime? Fecha1 { get; set; }
        public DateTime? Fecha2 { get; set; }
        public DateTime? Fecha3 { get; set; }
        public DateTime? Fecha4 { get; set; }

       
        //objetivo clave y actividad control
        [Required(ErrorMessage = "Objetivo Clave es requerido")]
        public bool ObjetivoClave { get; set; }


        [Required(ErrorMessage = "Actividad Control es requerido")]
        public bool ActividadControl { get; set; }


        //supuestos y acciones a efectuar
        [Required(ErrorMessage = "Supuestos de Verificación es requerido")]
        public string Supuestos { get; set; }

        [Required(ErrorMessage = "Acciones de Verificación es requerido")]
        public string Acciones { get; set; }

        
        public string Especificaciones { get; set; }

        //Aplica indicador 
        [Required(ErrorMessage = "Aplica Indicador es requerido")]
        public bool AplicaIndicador { get; set; }

        //Estado
        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //intersepcion
        [Required(ErrorMessage = "Unidad Auditora es requerido")]
        public int UnidadAuditoraId { get; set; }

        //navegacion 
        [ForeignKey("UnidadAuditoraId")]
        public UnidadAuditora UnidadAuditora { get; set; }

        //intersepcion
        [Required(ErrorMessage = "Dirección General es requerido")]
        public int? DireccionGeneralId { get; set; }

        //navegacion 
        [ForeignKey("DireccionGeneralId")]
        public DireccionGeneral DireccionGeneral { get; set; }


        public bool IsValid()
        {
            return Fecha1.HasValue || Fecha2.HasValue || Fecha3.HasValue || Fecha4.HasValue;
        }
    }
    
}
