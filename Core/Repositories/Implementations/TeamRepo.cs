using Core.Context;
using Core.Entities;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class TeamRepo : GenericRepo<Team>, ITeamRepo
    {
        private readonly BasketBallClubDbContext _context;
        
        public TeamRepo(BasketBallClubDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetByClubIdAsync(Guid clubId)
        {
            return await _context.Teams.Where(x => x.ClubId == clubId).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams.OrderBy(x => x.Name).ToListAsync();
        }
    }
}