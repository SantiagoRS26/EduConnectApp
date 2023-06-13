using EduConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface IMatchService
    {
        Task<bool> CreateMatch(Request user1, Request user2);
    }
}
