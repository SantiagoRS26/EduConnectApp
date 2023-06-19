using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _security;

        public AuthService(IGenericRepository<User> repository, IConfiguration configuration, ISecurityService security, IGenericRepository<Role> roleRepository)
        {
            _repository = repository;
            _configuration = configuration;
            _security = security;
            _roleRepository = roleRepository;
        }
        public async Task<User> Authentication(User user)
        {
            var query = await _repository.GetAll();
            var userDb = await query.FirstOrDefaultAsync(x => x.Email == user.Email);
            if(userDb != null)
            {
                if(_security.VerifyPassword(user.Password, userDb.Password))
                {
                    return userDb;
                }
            }
            return null;
        }

        public string GenerateJwt(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(50),
                    signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateJwt(string token)
        {
            throw new NotImplementedException();
        }
    }
}
