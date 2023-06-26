using EduConnect.API.Models;
using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICollegeService _collegeService;
        public AccountController(IAuthService authService, IUserService userService, ICollegeService collegeService)
        {
            _authService = authService;
            _userService = userService;
            _collegeService = collegeService;
        }
        [HttpGet("validateJwt")]
        [Authorize]
        public IActionResult ValidateJwt()
        {
            return Ok();
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool response = await _userService.CreateUser(new User { Name = user.Name, LastName = user.LastName, Email = user.Email, Password = user.Password });

            if (!response) {
                return BadRequest(ModelState);
            }
            return StatusCode(200, user);
            
        }
        [HttpPatch("updateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserModel updatedUser)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var emailUser = HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var existingUser = await _userService.GetByEmail(emailUser);

            if (existingUser == null) return NotFound("El usuario no existe.");

            if (updatedUser.CollegeId.HasValue)
            {
                var collegeExists = await _collegeService.CollegeExists(updatedUser.CollegeId.ToString());
                if (!collegeExists)
                {
                    ModelState.AddModelError("CollegeId", "El ID del colegio proporcionado no existe.");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingUser.Name = updatedUser.Name ?? existingUser.Name;
            existingUser.LastName = updatedUser.LastName ?? existingUser.LastName;
            existingUser.Email = updatedUser.Email ?? existingUser.Email;
            existingUser.Password = updatedUser.Password ?? existingUser.Password;
            existingUser.CollegeId = updatedUser.CollegeId ?? existingUser.CollegeId;

            // Guardar los cambios en la base de datos
            await _userService.Update(existingUser);

            return Ok("Usuario actualizado exitosamente.");
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
        public async Task<IActionResult> UserData()
        {
            var emailUser = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            if(emailUser == null) return BadRequest(ModelState);
            User userData = await _userService.GetByEmail(emailUser);

            object response = new
            {
                Email = userData.Email,
                Name = userData.Name,
                LastName = userData.LastName,
                Photo = userData.Photo,
                Role = userData.Role.RoleName,
                CollegeId = userData.CollegeId
            };

            return Ok(response);
        }

        [HttpPost("uploadprofilepicture")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha proporcionado ningún archivo.");
            }

            const int MAX_IMAGE_SIZE_IN_BYTES = 5 * 1024 * 1024; // 5 MB

            if (file.Length > MAX_IMAGE_SIZE_IN_BYTES)
            {
                return BadRequest("La imagen supera el tamaño permitido.");
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(file.FileName);
            bool validExtension = validExtensions.Contains(extension.ToLower());

            if (!validExtension)
            {
                return BadRequest("Formato de imagen no válido.");
            }

            var userEmailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return BadRequest("Token Invalido.");
            }

            string userEmail = userEmailClaim.Value;

            User user = await _userService.GetByEmail(userEmail);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            string folderPath = "pictureProfiles";
            string userId = user.UserId.ToString();
            string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{file.FileName}";
            string userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, userId);

            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            if (user.Photo != null)
            {
                string existingFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, userId);
                string existingFileName = Path.GetFileName(user.Photo);
                string existingFilePath = Path.Combine(existingFolderPath, existingFileName);


                // Eliminar la imagen existente si ya tiene una
                if (!string.IsNullOrEmpty(user.Photo) && System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }
            

            string filePath = Path.Combine(userFolderPath, fileName);

            try
            {
                // Guarda la ruta relativa de la foto, que puede ser usada para formar una URL en el cliente
                user.Photo = $"{userId}/{fileName}";
                await _userService.Update(user);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok("Imagen Subida Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
