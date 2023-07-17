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
                Status = "Pendiente",
                CreatedDate = DateTime.UtcNow
            };

            var flagRequest = await _repositoryRequest.Create(request);
            if (flagRequest) return true;
            return false;
        }

        public async Task<Request> FindMatchingRequests(Guid userId, Guid collegeId)
        {
            var userRequests = await GetRequestsByUserId(userId);

            var userRequest = userRequests.FirstOrDefault(p => p.CollegeId==collegeId);

            var user = await _repositoryUser.GetById(userId.ToString());

            var collegeRequests = await GetRequestsByCollegeId(user.CollegeId ?? Guid.Empty,user.UserId);

            var matchingCollegeRequests = collegeRequests.OrderBy(request => request.CreatedDate).FirstOrDefault(request => request.User.CollegeId == collegeId);


            if (matchingCollegeRequests != null)
            {
                matchingCollegeRequests.Status = "En Proceso";
                userRequest.Status = "En Proceso";

                var updateRequest1 = _repositoryRequest.Update(matchingCollegeRequests);
                var updateRequest2 = _repositoryRequest.Update(userRequest);
            }

            return matchingCollegeRequests;
        }

        public async Task<IQueryable<Request>> GetRequestsByUserId(Guid userId)
        {
            var query = await _repositoryRequest.GetAll();
            var result = query.Where(request => request.UserId == userId);
            return result;
        }

        public async Task<IQueryable<Request>> GetUsersByRequestId(Guid userId)
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

        public async Task<object> GetRequests(Guid userId)
        {
            var query = await GetRequestsByUserId(userId);
            var result = query.Select(request => new
            {
                request.CreatedDate,
                request.Status,
                request.College
            });
            return result;
        }
    }
}
