using EduConnect.BLL.Interfaces;
using EduConnect.DAL.Interface;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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

        public async Task<College> GetCollegeById(string collegeId)
        {
            var school = await _repository.GetById(collegeId) ?? new College();
            return school;
        }

        public async Task<IQueryable<College>> GetCollegesWithinRadius(Geometry circle)
        {
            var query = await _repository.GetAll();

            var filteredColleges = query.AsEnumerable()
                .Where(c => c.Latitude.HasValue && c.Longitude.HasValue &&
                            circle.Contains(new Point((double)c.Longitude.Value, (double)c.Latitude.Value) { SRID = 4326 }));

            return filteredColleges.AsQueryable();
        }

        public async Task<IQueryable<object>> GetAll()
        {
            var query = await _repository.GetAll();
            var result = query.Select(college => new
            {
                college.CollegeId,
                college.Name,
                college.Address,
                college.Latitude,
                college.Longitude,
                college.AdditionalInfo,
                college.AvailableSlots,
                college.CityId
            });

            return result;
        }




    }
}
