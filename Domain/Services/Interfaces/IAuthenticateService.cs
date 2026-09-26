using Core.Entities;

namespace Domain.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<User> SignInWithGoogleAsync(string externalId, string email, string displayName);
        Task<User> SignInWithMicrosoftAsync(string externalId, string email, string displayName);
    }
}