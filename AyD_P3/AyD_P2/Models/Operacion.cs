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
        [Display(Name = "Número de Cuenta")]
        public string Cuenta { get; set; }

        [Required]
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