using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Repositories
{
    public class ChatMessageRepository : IGenericRepository<ChatMessage>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public ChatMessageRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<ChatMessage>> GetAll()
        {
            IQueryable<ChatMessage> response = _dbContext.ChatMessages;
            return response;
        }

        public async Task<bool> Create(ChatMessage entityModel)
        {
            try
            {
                _dbContext.ChatMessages.Add(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var chatMessage = await _dbContext.ChatMessages.FindAsync(id);
                if (chatMessage == null)
                    return false;

                _dbContext.ChatMessages.Remove(chatMessage);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(ChatMessage entityModel)
        {
            try
            {
                _dbContext.Update(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<ChatMessage> GetById(string id)
        {
            return await _dbContext.ChatMessages.FindAsync(new Guid(id));
        }
    }
}
