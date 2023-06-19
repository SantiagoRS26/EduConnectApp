using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<User> Authentication(User user);
        string GenerateJwt(User user);
        bool ValidateJwt(string token);
    }
}
