using System.ComponentModel.DataAnnotations;

namespace EduConnect.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[A-Z]).{8,}$",
            ErrorMessage = "La contraseña debe contener al menos 8 caracteres y una letra mayúscula")]
        public string? Password { get; set; }
    }
}
