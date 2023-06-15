using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<ChatMessage> _chatMessageRepository;
        public ChatService(IGenericRepository<Chat> chatRepository, IGenericRepository<ChatMessage> chatMessageRepository) 
        { 
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<bool> CreateNewChat(Chat entity)
        {
            var newChat = await _chatRepository.Create(entity);
            if (newChat) return true;
            return false;
        }

        public async Task<IQueryable<ChatMessage>> GetChatMessages(Guid chatId)
        {
            var query = await _chatMessageRepository.GetAll();
            var response = query.Where(p => p.ChatId == chatId)
                                .OrderBy(p => p.SentDate);
            return response;
        }

        public async Task<IQueryable<Request>> GetRequestIdsByChatId(Guid chatId)
        {
            var query = await _chatRepository.GetById(chatId.ToString());

            var request1 = query.RequestId1 ?? Guid.Empty;
            var request2 = query.RequestId2 ?? Guid.Empty;

            var requests = new List<Request>
            {
                new Request { RequestId = request1 },
                new Request { RequestId = request2 }
            };

            return requests.AsQueryable();
        }

        public async Task<bool> UserBelongsChat(Guid userId, Guid chatId)
        {
            var Requests = await GetRequestIdsByChatId(chatId);

            var Response = Requests.FirstOrDefault(p => p.UserId == userId);
            if(Response == null) return false;
            return true;
        }

        public async Task<bool> SaveMessage(ChatMessage message)
        {
            var flag = await _chatMessageRepository.Create(message);
            if (flag != null) return flag;
            return false;
        }
    }
}
