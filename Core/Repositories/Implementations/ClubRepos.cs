using Core.Context;
using Core.Entities;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class ClubRepo : GenericRepo<Club>, IClubRepo
    {
        public ClubRepo(BasketBallClubDbContext context) : base(context)
        {
        }

        public async Task<List<Club>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
    }
}