//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AyD_P2
{
    using System;
    using System.Collections.Generic;
    
    public partial class CUENTA
    {
        public int cod_cuenta { get; set; }
        public string no_cuenta { get; set; }
        public string tipo { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public int cod_cliente { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
