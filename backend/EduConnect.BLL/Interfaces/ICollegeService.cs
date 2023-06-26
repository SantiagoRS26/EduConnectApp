using EduConnect.Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface ICollegeService
    {
        Task<bool> CollegeExists(string collegeId);
        Task<College> GetCollegeById(string collegeId);
        Task<IQueryable<College>> GetCollegesWithinRadius(Geometry circle);
        Task<IQueryable<Object>> GetAll();
    }
}
