using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.DAL.Interface
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<IQueryable<TEntityModel>> GetAll();
        Task<bool> Create(TEntityModel entityModel);
        Task<bool> Delete(int id);
        Task<bool> Update(TEntityModel entityModel);
        Task<TEntityModel> GetById(string id);
    }
}
