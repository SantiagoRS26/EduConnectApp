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
    public class ChatRepository : IGenericRepository<Chat>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public ChatRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Chat>> GetAll()
        {
            return _dbContext.Chats;
        }

        public async Task<bool> Create(Chat entityModel)
        {
            try
            {
                _dbContext.Chats.Add(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var chat = await _dbContext.Chats.FindAsync(id);
                if (chat == null)
                    return false;

                _dbContext.Chats.Remove(chat);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(Chat entityModel)
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

        public async Task<Chat> GetById(string id)
        {
            return await _dbContext.Chats.FindAsync(new Guid(id));
        }
    }

}
