using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using Core.Enums;

namespace Domain.Services.Implementations
{
    public class UserService : GenericService<User, Core.Entities.User>, IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITeamRepo _teamRepo;

        public UserService(IUserRepo userRepo, ITeamRepo teamRepo, IGenericRepo<Core.Entities.User> repo, IMapper<Core.Entities.User, User> domainMapper, IMapper<User, Core.Entities.User> entityMapper)
            : base(repo, domainMapper, entityMapper)
        {
            _userRepo = userRepo;
            _teamRepo = teamRepo;
        }

        public async Task<User> AuthenticateAsync(AuthenticationProvider provider, string externalId, string email, string displayName, string? profileImageUrl)
        {
            var existingUser = await _userRepo.GetByExternalLoginAsync(provider, externalId);

            if (existingUser is not null) return _toDomainMapper.Map(existingUser);

            var user = new User();

            user.ChangeEmail(email);
            user.ChangeDisplayName(displayName);
            user.ChangeProfileImage(profileImageUrl);
            user.SetExternalLogin(provider, externalId);

            await base.CreateAsync(user);

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user is null) throw new NotFoundException($"User with email: {email} not found");
            return _toDomainMapper.Map(user);
        }

        public override async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _userRepo.GetByIdAsync(user.Id) ?? throw new NotFoundException($"User with ID '{user.Id}' was not found.");
            if (user.TeamId.HasValue)
            {
                var team = await _teamRepo.GetByIdAsync(user.TeamId.Value) ?? throw new NotFoundException($"Team with ID {user.TeamId} was not found");
                if (!user.ClubId.HasValue || team.ClubId != user.ClubId.Value) throw new ValidationException("The selected team does not belong to the user's club");
            }

            existingUser.Email = user.Email;
            existingUser.DisplayName = user.DisplayName;
            existingUser.ClubId = user.ClubId;
            existingUser.TeamId = user.TeamId;
            existingUser.IsAdmin = user.IsAdmin;
            existingUser.ProfileImageUrl = user.ProfileImageUrl;

            await _userRepo.UpdateAsync(existingUser);
            await _userRepo.SaveChangesAsync();

            return user;
        }
    }
}