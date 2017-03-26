using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AyD_P2.Models
{
    //Campos de Logueo
    public class LoginViewModel
    {
        [Required]
        [Key]
        [Display(Name = "Código Usuario")]
        public string CodigoUsuario { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    
}
