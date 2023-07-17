using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EduConnect.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        [HttpGet("google-login")]
        [AllowAnonymous] // No se requiere autenticación para iniciar el proceso de autenticación con Google
        public IActionResult GoogleLogin()
        {
            // Redirige al usuario a la página de inicio de sesión de Google para autenticarse
            var properties = new AuthenticationProperties { RedirectUri = "/auth/google-callback" };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                // Ocurrió un error durante la autenticación con Google
                // Puedes manejar el error de la forma que desees
                return BadRequest("Error durante la autenticación con Google.");
            }

            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                // La autenticación con Google no tuvo éxito
                // Puedes manejar el fallo de autenticación de la forma que desees
                return BadRequest("Fallo la autenticación con Google.");
            }

            // La autenticación con Google fue exitosa
            // Aquí puedes obtener la información del usuario y generar el token JWT si es necesario
            var user = authenticateResult.Principal;
            // Obtener los datos del usuario, como el correo electrónico, nombre, etc.
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;
            var pictureUrl = user.FindFirst("picture")?.Value;

            // Generar el token JWT si es necesario
            //var token = GenerateJwtToken(email, name); // Implementa tu propia lógica para generar el token JWT

            // Puedes redirigir al usuario a una página de éxito o devolver el token JWT como respuesta
            // En este ejemplo, se devuelve el token JWT en formato JSON
            return Ok();
        }




    }
}
