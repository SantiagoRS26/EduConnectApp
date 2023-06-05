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
    public class RequestRepository : IGenericRepository<Request>
    {
        private readonly EduConnectPruebasContext _dbContext;
        public RequestRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Request>> GetAll()
        {
            return _dbContext.Requests;
        }

        public async Task<bool> Create(Request entityModel)
        {
            try
            {
                _dbContext.Requests.Add(entityModel);
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
                var request = await _dbContext.Requests.FindAsync(id);
                if (request == null)
                    return false;

                _dbContext.Requests.Remove(request);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(Request entityModel)
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

        public async Task<Request> GetById(string id)
        {
            return await _dbContext.Requests.FindAsync(id);
        }
    }
}
