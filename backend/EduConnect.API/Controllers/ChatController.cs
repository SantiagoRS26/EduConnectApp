using EduConnect.BLL.Interfaces;
using EduConnect.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

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

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IgnoreReadOnlyProperties = true
            };

            var json = JsonSerializer.Serialize(messagesChat, jsonOptions);

            return Ok(json);
        }

        [HttpGet("chats")]
        [Authorize]
        public async Task<IActionResult> GetChats()
        {
            var emailUser = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetByEmail(emailUser);
            var chatsUser = await _userService.GetChats(user);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var json = JsonSerializer.Serialize(chatsUser, jsonOptions);
            return Ok(json);
        }

    }
}
