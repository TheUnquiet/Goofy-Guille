using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class ClubService : IClubService
    {
        private readonly IClubRepo _clubRepo;
        private readonly IMapper<Core.Entities.Club, Models.Club> _toDomainMapper;
        private readonly IMapper<Models.Club, Core.Entities.Club> _toEntityMapper;

        public ClubService(IClubRepo clubRepo, IMapper<Core.Entities.Club, Models.Club> toDomainMapper, IMapper<Models.Club, Core.Entities.Club> toEntityMapper)
        {
            _clubRepo = clubRepo;
            _toDomainMapper = toDomainMapper;
            _toEntityMapper = toEntityMapper;
        }

        public async Task<Models.Club> GetByIdAsync(Guid id)
        {
            var club = await _clubRepo.GetByIdAsync(id);
            return club is null ? throw new NotFoundException($"Club: {id} not found") : _toDomainMapper.Map(club);
        }

        public async Task<List<Models.Club>> GetAllAsync()
        {
            var clubs = await _clubRepo.GetAllAsync();
            return clubs.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<Models.Club> CreateAsync(Models.Club club)
        {
            var entity = _toEntityMapper.Map(club);

            await _clubRepo.AddAsync(entity);
            await _clubRepo.SaveChangesAsync();

            return club;
        }

        public async Task<Models.Club> UpdateAsync(Models.Club club)
        {
            var existingClub = await _clubRepo.GetByIdAsync(club.Id) ?? throw new NotFoundException($"Club with ID '{club.Id}' was not found.");
            existingClub.Name = club.Name;

            await _clubRepo.UpdateAsync(existingClub);
            await _clubRepo.SaveChangesAsync();

            return club;
        }
    }
}
