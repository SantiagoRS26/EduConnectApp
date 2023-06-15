    using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduConnect.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<Role> _repositoryRole;
        private readonly ISecurityService _securityService;
        private readonly IRequestService _requestService;
        private readonly IChatService _chatService;
        
        public UserService(IChatService chatService, IRequestService requestService, IGenericRepository<User> repository, IConfiguration configuration, ISecurityService securityService, IGenericRepository<Role>  repositoryRole)
        {
            _repository = repository;
            _securityService = securityService;
            _repositoryRole = repositoryRole;
            _requestService = requestService;
            _chatService = chatService;
        }

        public async Task<bool> CreateUser(User entityModel)
        {
            try
            {
                entityModel.Password = _securityService.EncryptPassword(entityModel.Password);
                entityModel.RoleId = new Guid(await getRoleIdUser());
                await _repository.Create(entityModel);
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> getRoleIdUser()
        {
            var query = await _repositoryRole.GetAll();
            var role  = query.FirstOrDefault(p => p.RoleName=="user");
            if (role == null) return "";
            return role.RoleId.ToString();
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await _repository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userData = await _repository.GetAll();
            var data = userData.First(p => p.Email == email);
            return data;
        }

        public async Task<User> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(User entityModel)
        {
            try
            {
                await _repository.Update(entityModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<object>> GetChats(User user)
        {
            var queryRequest = await _requestService.GetRequestsByUserId(user.UserId);
            var queryChat = await _chatService.GetAll();

            var requestIds = queryRequest.Select(request => request.RequestId).ToList();

            var response = queryChat.Where(chat => requestIds.Contains(chat.RequestId1 ?? new Guid()) ||
                                                    requestIds.Contains(chat.RequestId2 ?? new Guid()));

            response = response.Include(chat => chat.RequestId1Navigation)
                       .Include(chat => chat.RequestId2Navigation);

            var projectedResponse = response.Select(chat => new
            {
                chat.ChatId,
                chat.CreatedDate,
                OtherUserId = chat.RequestId1Navigation.UserId == user.UserId ? chat.RequestId2Navigation.UserId : chat.RequestId1Navigation.UserId,
                name = chat.RequestId1Navigation.UserId == user.UserId ?
                        $"{chat.RequestId2Navigation.User.Name} {chat.RequestId2Navigation.User.LastName}" :
                        $"{chat.RequestId1Navigation.User.Name} {chat.RequestId1Navigation.User.LastName}"
            }
            );


            return projectedResponse.AsQueryable();
        }
    }
}
