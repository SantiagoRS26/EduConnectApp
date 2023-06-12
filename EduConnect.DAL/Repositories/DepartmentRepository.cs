using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Repositories
{
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public DepartmentRepository(EduConnectPruebasContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Create(Department entityModel)
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                Department department = await _dbContext.Departments.FindAsync(id) ?? new Department();
                if (department == null)
                    return false;

                _dbContext.Departments.Remove(department);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<Department>> GetAll()
        {
            IQueryable<Department> entityModels = _dbContext.Departments;
            return entityModels;
        }

        public async Task<Department> GetById(string id)
        {
            return await _dbContext.Departments.FindAsync(id) ?? new Department();
        }

        public async Task<bool> Update(Department entityModel)
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
