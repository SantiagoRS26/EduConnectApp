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
    public class MatchRepository : IGenericRepository<Match>
    {
        private readonly EduConnectPruebasContext _dbContext;

        public MatchRepository(EduConnectPruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Match>> GetAll()
        {
            return _dbContext.Matches;
        }

        public async Task<bool> Create(Match entityModel)
        {
            try
            {
                _dbContext.Matches.Add(entityModel);
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
                var match = await _dbContext.Matches.FindAsync(id);
                if (match == null)
                    return false;

                _dbContext.Matches.Remove(match);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar cualquier excepción y realizar el manejo de errores adecuado
                return false;
            }
        }

        public async Task<bool> Update(Match entityModel)
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

        public async Task<Match> GetById(string id)
        {
            return await _dbContext.Matches.FindAsync(id);
        }
    }
}
