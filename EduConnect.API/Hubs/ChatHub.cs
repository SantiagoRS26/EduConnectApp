using EduConnect.BLL.Interfaces;
using EduConnect.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EduConnect.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IConnectionService _connectionService;
        private readonly IRequestService _requestService;

        public ChatHub(IChatService chatService, IUserService userService, IConnectionService connectionService, IRequestService requestService)
        {   
            _chatService = chatService;
            _userService = userService;
            _connectionService = connectionService;
            _requestService = requestService;
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetByEmail(userEmail);
            var connection = Context.ConnectionId;
            await _connectionService.DeleteConnection(connection);

            await base.OnDisconnectedAsync(exception);
        }
        public override async Task OnConnectedAsync()
        {
            var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetByEmail(userEmail);
            var connection = new Connection()
            {
                ConnectionId = Context.ConnectionId,
                UserId = user.UserId
            };
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(Guid chatId, string content)
        {
            try
            {
                var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;

                var user = await _userService.GetByEmail(userEmail);

                var requests = await _chatService.GetRequestIdsByChatId(chatId);

                var senderRequest = requests.FirstOrDefault(r => r.RequestId == user.UserId);
                if (senderRequest == null)
                {
                    Console.WriteLine("No hay Request en el chat proporcionado");
                }

                var receiverRequest = requests.FirstOrDefault(r => r.RequestId != user.UserId);

                // Guardar el mensaje en la base de datos
                var message = new ChatMessage
                {
                    ChatId = chatId,
                    SenderId = user.UserId,
                    Message = content,
                    SentDate = DateTime.UtcNow
                };

                await _chatService.SaveMessage(message);

                var connectionsReceiver = await _connectionService.GetConnectionsByUserId(receiverRequest.UserId ?? new Guid());

                foreach (var connection in connectionsReceiver)
                {
                    await Clients.Client(connection.ConnectionId).SendAsync("ReceiveMessage", content);
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("MessageSaveFailed", $"Ocurrió un error al guardar el mensaje: {ex.Message}");
            }
        }
    }
}
