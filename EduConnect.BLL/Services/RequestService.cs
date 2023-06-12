using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.DAL.Repositories;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepository<Request> _repositoryRequest;
        private readonly IGenericRepository<User> _repositoryUser;

        public RequestService(IGenericRepository<Request> repositoryRequest,IGenericRepository<User> repositoryUser)
        {
            _repositoryRequest = repositoryRequest;
            _repositoryUser = repositoryUser;
        }

        public async Task<bool> CreateRequest(Guid userId, Guid collegeId)
        {
            var request = new Request()
            {
                UserId = userId,
                CollegeId = collegeId,
                Status = "Pendiente"
            };

            var flagRequest = await _repositoryRequest.Create(request);
            if (flagRequest) return true;
            return false;
        }

        public async Task<IQueryable<Request>> FindMatchingRequests(Guid userId, Guid collegeId)
        {
            var userRequests = await GetRequestsByUserId(userId);

            var userRequest = userRequests.FirstOrDefault(p => p.CollegeId==collegeId);

            var user = await _repositoryUser.GetById(userId.ToString());

            var collegeRequests = await GetRequestsByCollegeId(user.CollegeId ?? Guid.Empty,user.UserId);

            var matchingCollegeRequests = collegeRequests.Where(request => request.User.CollegeId == collegeId);

            return matchingCollegeRequests;
        }

        public async Task<IQueryable<Request>> GetRequestsByUserId(Guid userId)
        {
            var query = await _repositoryRequest.GetAll();
            var result = query.Where(request => request.UserId == userId);
            return result;
        }

        public async Task<IQueryable<Request>> GetRequestsByCollegeId(Guid collegeId, Guid userId)
        {
            var query = await _repositoryRequest.GetAll();
            var response = query.Where(p => p.CollegeId == collegeId && p.UserId != userId && p.Status == "Pendiente");
            return response;
        }

        public Task<bool> FindMatch(string userId, string collegeId)
        {
            throw new NotImplementedException();
        }
    }
}
