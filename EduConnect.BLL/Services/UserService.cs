using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
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

namespace EduConnect.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly ISecurityService _securityService;
        
        public UserService(IGenericRepository<User> repository, IConfiguration configuration, ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
        }

        public async Task<bool> CreateUser(User entityModel)
        {
            try
            {
                entityModel.Password = _securityService.EncryptPassword(entityModel.Password);
                entityModel.RoleId = new Guid("41C056BA-EF76-419F-8BC0-3CAE439D7D5B");
                await _repository.Create(entityModel);
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
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

        public async Task<User> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(User entityModel)
        {
            throw new NotImplementedException();
        }
    }
}
