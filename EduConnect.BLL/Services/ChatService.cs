using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.DAL.Repositories;
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
        private readonly IGenericRepository<Request> _requestRepository;
        public ChatService(IGenericRepository<Chat> chatRepository, IGenericRepository<ChatMessage> chatMessageRepository,IGenericRepository<Request> requestRepository) 
        { 
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
            _requestRepository = requestRepository;
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

        public async Task<IQueryable<Request>> GetRequestByChatId(Guid chatId)
        {
            var queryChats = await _chatRepository.GetAll();
            var chat = queryChats.FirstOrDefault(p => p.ChatId == chatId);

            var Request = await _requestRepository.GetAll();
            var responseRequest = Request.Where(p => p.RequestId == chat.RequestId1 || p.RequestId == chat.RequestId2);
            
            return responseRequest;

        }

        public async Task<IQueryable<User>> GetUsersByChatId(Guid chatId)
        {
            var queryChats = await _chatRepository.GetAll();
            var chat = queryChats.FirstOrDefault(p => p.ChatId == chatId);

            var Requests = await _requestRepository.GetAll();
            var responseRequest = Requests.Where(p => p.RequestId == chat.RequestId1 || p.RequestId == chat.RequestId2);

            var users = new List<User>();
            foreach (var request in responseRequest)
            {
                if (request.User != null)
                {
                    users.Add(request.User);
                }
            }
            return users.AsQueryable();
        }


        public async Task<bool> UserBelongsChat(Guid userId, Guid chatId)
        {
            var Requests = await GetRequestByChatId(chatId);

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

        public async Task<IQueryable<Chat>> GetAll()
        {
            var response = await _chatRepository.GetAll();
            return response;
        }
    }
}
