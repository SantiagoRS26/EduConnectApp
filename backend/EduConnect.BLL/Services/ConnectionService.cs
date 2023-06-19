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
    public class ConnectionService : IConnectionService
    {
        private readonly IGenericRepository<Connection> _repository;
        public ConnectionService(IGenericRepository<Connection> repository)
        {
            _repository = repository;
        }
        public async Task<bool> CreateNewConnection(Connection entity)
        {
            var createConnection = await _repository.Create(entity);
            if(createConnection) return true;
            return false;
        }

        public async Task<bool> DeleteConnection(string connectionId)
        {
            var deleteConnection = await _repository.Delete(connectionId);
            if(deleteConnection) return true;
            return false;
        }

        public async Task<IQueryable<Connection>> GetConnectionsByUserId(Guid idUser)
        {
            var query = await _repository.GetAll();
            var response = query.Where(p => p.UserId==idUser);
            return response;
        }
    }
}
