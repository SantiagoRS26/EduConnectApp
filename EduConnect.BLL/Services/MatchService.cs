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
    public class MatchService : IMatchService
    {
        private readonly IGenericRepository<Match> _matchRepository;
        public MatchService(IGenericRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public async Task<bool> CreateMatch(Request user1, Request user2)
        {
            Match newMatch = new Match()
            {
                RequestIdUser1 = user1.RequestId,
                RequestIdUser2 = user2.RequestId,
                CreatedDate = DateTime.UtcNow
            };
            if(await _matchRepository.Create(newMatch)) return true;
            return false;
        }
    }
}
