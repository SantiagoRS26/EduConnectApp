using EduConnect.BLL.Interfaces;
using EduConnect.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatController(IChatService chatService,IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [HttpGet("messages")]
        [Authorize]
        public async Task<IActionResult> GetMessages(string idChat)
        {
            var emailUser = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetByEmail(emailUser);

            var userBelongChat = await _chatService.UserBelongsChat(user.UserId,new Guid(idChat));
            var messagesChat = await _chatService.GetChatMessages(new Guid(idChat));
            return Ok(messagesChat);
        }
    }
}
