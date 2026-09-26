using Core.Entities;
using Core.Repositories.Interfaces;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user ?? throw new NotFoundException($"User: {id} not found");
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            return user ?? throw new NotFoundException($"User email: {email} not found");
        }

        public async Task<User> CreateAsync(User user)
        {
            var existingUser = await _userRepo.GetByEmailAsync(user.Email);

            if (existingUser is not null) throw new ValidationException($"A user with email '{user.Email}' already exists.");
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _userRepo.GetByIdAsync(user.Id) ?? throw new NotFoundException($"User with ID '{user.Id}' was not found.");
            existingUser.Email = user.Email;
            existingUser.DisplayName = user.DisplayName;
            existingUser.ClubId = user.ClubId;
            existingUser.IsAdmin = user.IsAdmin;
            existingUser.ExternalProvider = user.ExternalProvider;
            existingUser.ExternalId = user.ExternalId;

            await _userRepo.UpdateAsync(existingUser);
            await _userRepo.SaveChangesAsync();

            return existingUser;
        }
    }
}
