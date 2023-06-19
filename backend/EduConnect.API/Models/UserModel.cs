using System.ComponentModel.DataAnnotations;

namespace EduConnect.API.Models
{
    public class UserModel
    {
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "El email proporcionado no es válido.")]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]).{8,}$",
            ErrorMessage = "La contraseña debe contener al menos 8 caracteres y una letra mayúscula")]
        public string? Password { get; set; }

        public Guid? CollegeId { get; set; }
    }

}
