using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IRequestService
    {
        Task<bool> CreateRequest(Guid userId, Guid collegeId);
        Task<IQueryable<Request>> GetRequestsByUserId(Guid userId);
        Task<Request> FindMatchingRequests(Guid userId, Guid collegeId);
    }
}
