using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly IGenericRepository<College> _repository;
        public CollegeService(IGenericRepository<College> repository)
        {
            _repository = repository;
        }
        public async Task<bool> CollegeExists(string collegeId)
        {
            var college = await _repository.GetById(collegeId);
            
            return college != null;
        }
    }
}
