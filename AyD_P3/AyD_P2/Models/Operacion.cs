using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AyD_P2.Models
{
    

    public class Operacion
    {

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese número de cuenta válido")]
        [Display(Name = "Número de Cuenta")]
        public string Cuenta { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Ingrese monto válido")]
        [StringLength(5, ErrorMessage = "El número de cuenta es de 5 dígitos", MinimumLength = 5)]
        [Display(Name = "Monto")]
        public string Monto { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Codigo Usuario")]
        public string Usuario { get; set; }
    }
}