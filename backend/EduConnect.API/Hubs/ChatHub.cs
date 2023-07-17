using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EduConnect.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;

        public ChatHub(IChatService chatService, IUserService userService, IRequestService requestService)
        {   
            _chatService = chatService;
            _userService = userService;
            _requestService = requestService;
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public override async Task OnConnectedAsync()
        {
            //var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;
            //var userIdd = Context.UserIdentifier;
            //var user = await _userService.GetByEmail(userEmail);
            //var connection = new Connection()
            //{
            //    ConnectionId = Context.ConnectionId,
            //    UserId = user.UserId
            //};
            //await _connectionService.CreateNewConnection(connection);
            await base.OnConnectedAsync();
         }
        public async Task SendMessage(Guid chatId, string content)
        {
            try
            {
                var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;

                var user = await _userService.GetByEmail(userEmail);

                // Obtener la zona horaria de Colombia
                var colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bogota");

                // Obtener la fecha y hora actual en la zona horaria de Colombia
                var currentDateTimeColombia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

                var users = await _chatService.GetUsersByChatId(chatId);

                var receiver = users.FirstOrDefault(p => p.UserId != user.UserId);

                // Guardar el mensaje en la base de datos
                var message = new ChatMessage
                {
                    ChatId = chatId,
                    SenderId = user.UserId,
                    Message = content,
                    SentDate = currentDateTimeColombia
                };

                await _chatService.SaveMessage(message);

                var messageData = new Dictionary<string, object>
                {
                    { "SenderId", message.SenderId },
                    { "Message", message.Message },
                    { "SentDate", message.SentDate }
                };

                await Clients.User(receiver.Email).SendAsync("ReceiveMessage", messageData);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("MessageSaveFailed", $"Ocurrió un error al guardar el mensaje: {ex.Message}");
            }
        }
    }
}
