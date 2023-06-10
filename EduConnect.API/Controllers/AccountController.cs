using EduConnect.API.Models;
using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLogin = await _authService.Authentication(new User { Email = user.Email, Password = user.Password });

            if (userLogin == null)
            {
                return NotFound();
            }

            var token = _authService.GenerateJwt(userLogin);
            return Ok(new { token = token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            if(ModelState.IsValid)
            {
                bool response = await _userService.CreateUser(new User { Name = user.Name, LastName = user.LastName, Email = user.Email, Password = user.Password  });
                if (response)
                {
                    return StatusCode(200, user);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("listar")]
        public IActionResult listar()
        {
            var lista = _userService.GetAll();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var jsonString = JsonSerializer.Serialize(lista, options);
            return Content(jsonString, "application/json");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserData(string emailUser)
        {
            User userData = await _userService.GetByEmail(emailUser);
            var response = new User
            {
                Email = userData.Email,
                Name = userData.Name,
                LastName = userData.LastName,
                Photo = userData.Photo,
            };
            return Ok(response);
        }

        [HttpPost("uploadprofilepicture")]
        [Authorize]
        public IActionResult UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ModelState);
            }
            
        }

    }
}
