using Core.Entities;
using Core.Enums;

namespace Core.Repositories.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByExtrernalLoginAsync(AuthenticationProvider provider, string emailId);
        Task<List<User>> GetByClubIdAsync(Guid clubId);
    }
}