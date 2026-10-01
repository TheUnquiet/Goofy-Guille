using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class LocationService : GenericService<Location, Core.Entities.Location>, ILocationService
    {
        private readonly ILocationRepo _locationRepo;

        public LocationService(ILocationRepo locationRepo,
            IGenericRepo<Core.Entities.Location> repo,
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