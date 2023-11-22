using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Dtos.Auth
{
    public class LoginDto
    {
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "la {0} es requerida")]
        public string Password { get; set; }
    }
}
