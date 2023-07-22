using EduConnect.API.Hubs;
using EduConnect.API.Models;
using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IChatService _chatService;

        public RequestController(IUserService userService, ICollegeService collegeService, IRequestService requestService, IHubContext<ChatHub> chatHubContext, IChatService chatService)
        {
            _userService = userService;
            _collegeService = collegeService;
            _requestService = requestService;
            _chatHubContext = chatHubContext;
            _chatService = chatService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRequests()
        {
            var userEmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
            var user = await _userService.GetByEmail(userEmailClaim.Value);
            var requests = await _requestService.GetRequests(user.UserId);
            if (requests == null)
            {
                return StatusCode(204);
            }
            return Ok(requests);
        }


        [HttpPost("sendRequest")]
        [Authorize]
        public async Task<IActionResult> SendRequest(CollegeModel college)
        {
            var userEmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
            
            var user = await _userService.GetByEmail(userEmailClaim.Value);
            if (user.CollegeId == null) return BadRequest("Por favor primero seleccione en su perfil el colegio donde se encuentra.");

            string collegeId = college.CollegeId;
            var collegeExists = await _collegeService.CollegeExists(collegeId);
            if (!collegeExists) return NotFound("El colegio ingresado no existe en la base de datos.");

            if (collegeId == user.CollegeId.ToString()) return BadRequest("No puedes enviar una solicitud a tu mismo colegio.");

            var userRequest = await _requestService.GetRequestsByUserId(user.UserId);
            var listUserRequest = userRequest.FirstOrDefault(p => p.CollegeId == new Guid(collegeId));
            if (listUserRequest != null) return Conflict("Ya tiene una solicitud para el colegio seleccionado");

            var createNewRequest = await _requestService.CreateRequest(user.UserId,new Guid(collegeId));
            if (!createNewRequest) return StatusCode(500,"No se pudo crear la solicitud, por favor intentalo mas tarde.");

            //Aqui se deberia buscar coincidencias a partir de la nueva solicitud
            var matchingCollegeRequests = await _requestService.FindMatchingRequests(user.UserId, new Guid(collegeId));
            if(matchingCollegeRequests != null)
            {
                var requestsUser = await _requestService.GetRequestsByUserId(user.UserId);
                var requestUser = requestsUser.FirstOrDefault(p => p.CollegeId == new Guid(collegeId));

                var newChat = new Chat()
                {
                    RequestId1 = matchingCollegeRequests.RequestId,
                    RequestId2 = requestUser.RequestId,
                    CreatedDate = DateTime.UtcNow
                };
                var flagNewChat = await _chatService.CreateNewChat(newChat);
                if (flagNewChat) return Ok("Se creo la solicitud y ya tienes un match! Verifica en la pestaña de chats");
                return Ok("Se creo la solicitud pero no se pudo crear el match.");
            }
            return Ok("Se ha creado la solicitud correctamente!");
        }

    }
}
