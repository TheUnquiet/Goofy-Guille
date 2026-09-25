using Core.Context;
using Core.Entities;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class EventRepo : GenericRepo<Event>, IEventRepo
    {
        public EventRepo(BasketBallClubDbContext context) : base(context)
        {
        }

        public async Task<List<Event>> GetByClubIdAsync(Guid clubId)
        {
            return await dbSet.Where(x => x.ClubId == clubId).ToListAsync();
        }

        public async Task<List<Event>> GetUpcomingByClubIdAsync(Guid clubId)
        {
            return await dbSet
                .Where(x => x.ClubId == clubId)
                .Where(x => x.StartTime >= DateTime.UtcNow)
                .OrderBy(x => x.StartTime).ToListAsync();
        }
    }
}