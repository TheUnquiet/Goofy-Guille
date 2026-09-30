using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class ClubService : GenericService<Club, Core.Entities.Club>, IClubService
    {
        private readonly IClubRepo _clubRepo;

        public ClubService(IClubRepo clubRepo, IMapper<Core.Entities.Club, Models.Club> domainMapper, 
            IMapper<Models.Club, Core.Entities.Club> entityMapper)
            : base(repo, domainMapper, entityMapper)
        {
            _clubRepo = clubRepo;
        }

        public async Task<List<Models.Club>> GetAllAsync()
        {
            var clubs = await _clubRepo.GetAllAsync();
            return clubs.Select(_toDomainMapper.Map).ToList();
        }Z
    }
}
