using Core.Context;
using Core.Entities;
using Core.Enums;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(BasketBallClubDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetByClubIdAsync(Guid clubId)
        {
            return await dbSet.Where(x => x.ClubId == clubId).ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByExternalLoginAsync(AuthenticationProvider provider, string externalId)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ExternalProvider == provider && x.ExternalId == externalId);
        }
    }
}