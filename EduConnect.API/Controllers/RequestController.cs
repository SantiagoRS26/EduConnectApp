using EduConnect.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICollegeService _collegeService;
        private readonly IRequestService _requestService;

        public RequestController(IUserService userService, ICollegeService collegeService, IRequestService requestService)
        {
            _userService = userService;
            _collegeService = collegeService;
            _requestService = requestService;
        }


        [HttpPost("sendRequest")]
        [Authorize]
        public async Task<IActionResult> SendRequest(string collegeId)
        {
            var userEmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return BadRequest("Token Invalido.");
            }
            var user = await _userService.GetByEmail(userEmailClaim.Value);
            if (user.CollegeId == null) return BadRequest("Por favor primero seleccione en su perfil el colegio donde se encuentra.");

            var collegeExists = await _collegeService.CollegeExists(collegeId);
            if (!collegeExists) return BadRequest("El colegio ingresado no existe en la base de datos.");

            if (collegeId == user.CollegeId.ToString()) return BadRequest("No puedes enviar una solicitud a tu mismo colegio.");

            var userRequest = await _requestService.GetRequestsByUserId(user.UserId);
            var listUserRequest = userRequest.FirstOrDefault(p => p.CollegeId == new Guid(collegeId));
            if (listUserRequest != null) return BadRequest("Ya tiene una solicitud al colegio seleccionado");

            var createNewRequest = await _requestService.CreateRequest(user.UserId,new Guid(collegeId));
            if (!createNewRequest) return BadRequest("No se pudo crear la solicitud, por favor intentalo mas tarde.");

            //Aqui se deberia buscar coincidencias a partir de la nueva solicitud
            var matchingCollegeRequests = _requestService.FindMatchingRequests(user.UserId, new Guid(collegeId));
            if(matchingCollegeRequests != null)
            {

            }

            return Ok("Se ha creado la solicitud correctamente!");
        }

    }
}
