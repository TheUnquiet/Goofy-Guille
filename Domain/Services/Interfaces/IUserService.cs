using Core.Enums;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IUserService : IGenericService<User, Core.Entities.User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> AuthenticateAsync(AuthenticationProvider provider, string externalId, string email, string displayName, string? profileImageUrl);
    }
}