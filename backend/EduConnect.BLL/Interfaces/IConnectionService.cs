using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IConnectionService
    {
        Task<bool> CreateNewConnection(Connection entity);
        Task<bool> DeleteConnection(string connectionId);
        Task<IQueryable<Connection>> GetConnectionsByUserId(Guid idUser);
    }
}
