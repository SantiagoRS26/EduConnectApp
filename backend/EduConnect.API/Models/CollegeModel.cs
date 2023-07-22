using System.ComponentModel.DataAnnotations;

namespace EduConnect.API.Models
{
    public class CollegeModel
    {
        [Required(ErrorMessage = "No se ha seleccionado ningun colegio")]
        public string CollegeId { get; set; }
    }
}
