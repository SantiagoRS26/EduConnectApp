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
    public class HistoryRepository : IGenericRepository<History>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public HistoryRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<History>> GetAll()
        {
            return _dbContext.Histories;
        }

        public async Task<bool> Create(History entityModel)
        {
            try
            {
                _dbContext.Histories.Add(entityModel);
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
                var history = await _dbContext.Histories.FindAsync(id);
                if (history == null)
                    return false;

                _dbContext.Histories.Remove(history);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(History entityModel)
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

        public async Task<History> GetById(string id)
        {
            return await _dbContext.Histories.FindAsync(new Guid(id));
        }
    }
}
