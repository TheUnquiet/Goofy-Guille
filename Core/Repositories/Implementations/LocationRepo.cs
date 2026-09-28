using Core.Entities;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class LocationRepo : GenericRepo<Location>, ILocationRepo
    {
        public LocationRepo(BasketBallClubDbContext context) : base(context)
        {
        }

        public async Task<List<Location>> GetAllAsync()
        {
            return await dbSet.OrderBy(x => x.Name).ToListAsync();
        }
    }
}