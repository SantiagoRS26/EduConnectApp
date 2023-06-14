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
        Task<bool> SaveMessage(Chat message);
        Task<IQueryable<Chat>> GetChatMessages(Guid chatId);
    }
}
