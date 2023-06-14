using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IGenericRepository<Chat> _chatRepository;
        public ChatService(IGenericRepository<Chat> chatRepository) 
        { 
            _chatRepository = chatRepository;
        }

        public async Task<IQueryable<Chat>> GetChatMessages(Guid chatId)
        {
            var query = await _chatRepository.GetAll();
            var response  = query.Where(p => p.ChatId == chatId);
            return response;
        }

        public async Task<bool> SaveMessage(Chat message)
        {
            var flag = await _chatRepository.Create(message);
            if (flag != null) return flag;
            return false;
        }
    }
}
