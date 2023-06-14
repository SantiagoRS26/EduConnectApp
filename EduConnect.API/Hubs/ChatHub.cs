using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EduConnect.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatHub(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(Guid chatId, string content)
        {
            // Obtener el remitente actualmente autenticado
            var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;

            var user = await _userService.GetByEmail(userEmail);


            // Guardar el mensaje en la base de datos
            var message = new Chat
            {
                ChatId = chatId,
                SenderId = user.UserId,
                SentDate = DateTime.UtcNow,
            };

            await _chatService.SaveMessage(message);

            // Enviar el mensaje a los miembros del grupo (usuarios en el chat)
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
