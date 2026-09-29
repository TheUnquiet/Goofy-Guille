using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITeamRepo _teamRepo;
        private readonly IMapper<Core.Entities.User, User> _toDomainMapper;
        private readonly IMapper<User, Core.Entities.User> _toEntityMapper;

        public UserService(IUserRepo userRepo, ITeamRepo teamRepo, IMapper<Core.Entities.User, User> toDomainMapper, IMapper<User, Core.Entities.User> toEntityMapper)
        {
            _userRepo = userRepo;
            _teamRepo = teamRepo;
            _toDomainMapper = toDomainMapper;
            _toEntityMapper = toEntityMapper;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user is null ? throw new NotFoundException($"User: {id} not found") : _toDomainMapper.Map(user);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user is null) throw new NotFoundException($"User with email: {email} not found");
            return _toDomainMapper.Map(user);
        }

        public async Task<User> CreateAsync(User user)
        {
            var existingUser = await _userRepo.GetByEmailAsync(user.Email);

            if (existingUser is not null) throw new ValidationException($"A user with email '{user.Email}' already exists.");
            var entity = _toEntityMapper.Map(user);
            await _userRepo.AddAsync(entity);
            await _userRepo.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _userRepo.GetByIdAsync(user.Id) ?? throw new NotFoundException($"User with ID '{user.Id}' was not found.");

            if (existingUser is null) throw new NotFoundException($"User with ID {user.Id} was not found");
            if (user.TeamId.HasValue)
            {
                var team = await _teamRepo.GetByUserIdAsync(user.TeamId.Value);
                if (team is null) throw new NotFoundException($"Team with ID {user.TeamId} was not found");
                if (!user.ClubId.HasValue || team.ClubId != user.ClubId.Value) throw new ValidationException("The selcted team does not belong to the user's club");
            }

            existingUser.Email = user.Email;
            existingUser.DisplayName = user.DisplayName;
            existingUser.ClubId = user.ClubId;
            existingUser.TeamId = user.TeamId;
            existingUser.IsAdmin = user.IsAdmin;

            await _userRepo.UpdateAsync(existingUser);
            await _userRepo.SaveChangesAsync();

            return user;
        }
    }
}