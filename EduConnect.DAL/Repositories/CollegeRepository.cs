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
    public class CollegeRepository : IGenericRepository<College>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public CollegeRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<College>> GetAll()
        {
            return _dbContext.Colleges;
        }

        public async Task<bool> Create(College entityModel)
        {
            try
            {
                _dbContext.Colleges.Add(entityModel);
                await _dbContext.SaveChangesAsync();
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
                var college = await _dbContext.Colleges.FindAsync(id);
                if (college == null)
                    return false;

                _dbContext.Colleges.Remove(college);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(College entityModel)
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

        public async Task<College> GetById(string id)
        {
            return await _dbContext.Colleges.FindAsync(id);
        }
    }

}
