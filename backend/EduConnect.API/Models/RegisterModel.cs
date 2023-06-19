using System.ComponentModel.DataAnnotations;

namespace EduConnect.API.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "El nombre debe ser obligatorio")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El apellido debe ser obligatorio")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[A-Z]).{8,}$",
            ErrorMessage = "La contraseña debe contener al menos 8 caracteres y una letra mayúscula")]
        public string? Password { get; set; }
    }
}
