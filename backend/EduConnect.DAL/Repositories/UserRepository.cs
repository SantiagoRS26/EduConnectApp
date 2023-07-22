using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Repositories
{
    public class UserRepository : IGenericRepository<User>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public UserRepository(EduConnectPruebasContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> Create(User entityModel)
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
                User user = await _dbContext.Users.FindAsync(id) ?? new User();
                if (user == null)
                    return false;

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<User>> GetAll()
        {
            IQueryable<User> entityModels = _dbContext.Users.Include(u => u.Role).Include(u => u.College);
            return entityModels;
        }

        public async Task<User> GetById(string id)
        {
            return await _dbContext.Users.FindAsync(new Guid(id)) ?? new User();
        }

        public async Task<bool> Update(User entityModel)
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
