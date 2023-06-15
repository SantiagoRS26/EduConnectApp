using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Repositories
{
    public class ConnectionRepository : IGenericRepository<Connection>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public ConnectionRepository(EduConnectPruebasContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Create(Connection entityModel)
        {
            try
            {
                _dbContext.Add(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                Connection connection = await _dbContext.Connections.FindAsync(id) ?? new Connection();
                if (connection == null)
                    return false;

                _dbContext.Connections.Remove(connection);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<Connection>> GetAll()
        {
            IQueryable<Connection> entityModels = _dbContext.Connections;
            return entityModels;
        }

        public async Task<Connection> GetById(string id)
        {
            return await _dbContext.Connections.FindAsync(id) ?? new Connection();
        }

        public async Task<bool> Update(Connection entityModel)
        {
            try
            {
                _dbContext.Update(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
