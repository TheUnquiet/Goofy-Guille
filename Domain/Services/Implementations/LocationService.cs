using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo _locationRepo;

        public LocationService(ILocationRepo locationRepo, 
            IMapper<Core.Entities.Location, Models.Location> domainMapper, 
            IMapper<Models.Location, Core.Entities.Location> entityMapper)
        : base(repo, domainMapper, entityMapper)
        {
            _locationRepo = locationRepo;
        }

        public async Task<List<Location>> GetAllAsync()
        {
            var locations = await _locationRepo.GetAllAsync();
            return locations.Select(_toDomainMapper.Map).ToList();
        }
    }
}