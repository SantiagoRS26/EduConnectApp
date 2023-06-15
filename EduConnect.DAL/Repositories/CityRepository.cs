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
    public class CityRepository : IGenericRepository<City>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public CityRepository(EduConnectPruebasContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Create(City entityModel)
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
                City city = await _dbContext.Cities.FindAsync(id) ?? new City();
                if (city == null)
                    return false;

                _dbContext.Cities.Remove(city);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<City>> GetAll()
        {
            IQueryable<City> entityModels = _dbContext.Cities;
            return entityModels;
        }

        public async Task<City> GetById(string id)
        {
            return await _dbContext.Cities.FindAsync(id) ?? new City();
        }

        public async Task<bool> Update(City entityModel)
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
