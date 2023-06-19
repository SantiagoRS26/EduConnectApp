using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ICollegeService _collegeService;
        public CollegesController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCollegesWithRadius(string idSchool, double radius)
        {
            var school = await _collegeService.GetCollegeById(idSchool);
            if (school == null) return NotFound("Colegio no encontrado, por favor verifique la información enviada");


            // Crear el punto de origen utilizando las coordenadas del colegio
            var originPoint = new Point((double)school.Longitude.Value, (double)school.Latitude.Value) { SRID = 4326 };

            // Calcular el radio en metros
            var radiusInMeters = radius * 1000; // Convertir de kilómetros a metros

            // Crear un círculo alrededor del punto de origen con el radio especificado
            var circle = originPoint.Buffer(radiusInMeters);

            // Consultar los colegios dentro del círculo utilizando NetTopologySuite
            var collegesWithinRadius = await _collegeService.GetCollegesWithinRadius(circle);

            return Ok(collegesWithinRadius);
        }
    }
}
