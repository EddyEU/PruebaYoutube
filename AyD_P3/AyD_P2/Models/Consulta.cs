using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AyD_P2.Models
{
    public class Consulta
    {
        [Display(Name = "Saldo")]
        public string saldo { get; set; }
    }
}