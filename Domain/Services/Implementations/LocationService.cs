namespace Domain.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo _locationRepo;
        private readonly IMapper<Core.Entities.Location, Models.Location> _toDomainMapper;
        private readonly IMapper<Models.Location, Core.Entities.Location> _toEntityMapper;

        public LocationService(ILocationRepo locationRepo, IMapper<Core.Entities.Location, Models.Location> domainMapper, IMapper<Models.Location, Core.Entities.Location> entityMapper)
        {
            _locationRepo = locationRepo;
            _toDomainMapper = domainMapper;
            _toEntityMapper = entityMapper;
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            var location = await _locationRepo.GetByIdAsync(id);
            if (location is null) throw new NotFoundException($"Location: {id} not found");
            return _toDomainMapper.Map(location);
        }

        public async Task<List<Location>> GetAllAsync()
        {
            var locations = await _locationRepo.GetAllAsync();
            return locations.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<Location> CreateAsync(Location location)
        {
            var entity = _toEntityMapper.Map(location);
            await _locationRepo.AddAsync(entity);
            await _locationRepo.SaveChangesAsync();
            return location;
        }

        public async Task<Location> UpdateAsync(Location location)
        {
            var existingLocation = await _locationRepo.GetByIdAsync(location.Id);
            if (existingLocation is null) throw new NotFoundException($"Location with ID {location.Id} not found");
            existingLocation.Name = location.Name;
            existingLocation.Street = location.Street;
            existingLocation.StreetNumber = location.StreetNumber;
            existingLocation.PostalCode = location.PostalCode;
            existingLocation.City = location.City;
            await _locationRepo.UpdateAsync(existingLocation);
            await _locationRepo.SaveChangesAsync();
            return location;
        }
    }
}