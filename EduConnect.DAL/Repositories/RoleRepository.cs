    using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Repositories
{
    public class RoleRepository : IGenericRepository<Role>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public RoleRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Role>> GetAll()
        {
            return _dbContext.Roles;
        }

        public async Task<bool> Create(Role entityModel)
        {
            try
            {
                _dbContext.Roles.Add(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);
                if (role == null)
                    return false;

                _dbContext.Roles.Remove(role);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(Role entityModel)
        {
            try
            {
                _dbContext.Update(entityModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<Role> GetById(string id)
        {
            return await _dbContext.Roles.FindAsync(new Guid(id)) ?? new Role();
        }
    }

}
