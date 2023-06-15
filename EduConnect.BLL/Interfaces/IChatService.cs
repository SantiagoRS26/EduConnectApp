using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IChatService
    {
        Task<bool> SaveMessage(ChatMessage message);
        Task<IQueryable<ChatMessage>> GetChatMessages(Guid chatId);
        Task<bool> CreateNewChat(Chat entity);
        Task<IQueryable<Request>> GetRequestByChatId(Guid chatId);
        Task<bool> UserBelongsChat(Guid userId, Guid chatId);
        Task<IQueryable<Chat>> GetAll();
    }
}
