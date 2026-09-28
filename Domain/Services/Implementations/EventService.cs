namespace Domain.Services.Implementations
{
    public class EventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IUserRepo _userRepo;
        private readonly IClubRepo _clubRepo;
        private readonly ILocationRepo _locationRepo;
        private readonly IMapper<Core.Entities.Event, Models.Event> _toDomainMapper;
        private readonly IMapper<Models.Event, Core.Entities.Event> _toEntityMapper;

        public EventService(IEventRepo eventRepo, IUserRepo userRepo, IClubRepo clubRepo, ILocationRepo locationRepo, IMapper<Core.Entities.Event, Models.Event> domainMapper, IMapper<Models.Event, Core.Entities.Event> entityMapper)
        {
            _eventRepo = eventRepo;
            _userRepo = userRepo;
            _clubRepo = clubRepo;
            _locationRepo = locationRepo;
            _toDomainMapper = domainMapper;
            _toEntityMapper = entityMapper;
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            var @event = await _eventRepo.GetByIdAsync(id);
            if (@event is null) throw new NotFoundException($"Event with ID {id} was not found");
            return _toDomainMapper.Map(@event);
        }

        public async Task<List<Models.Event>> GetByClubIdAsync(Guid clubId)
        {
            var events = await _eventRepo.GetByClubIdAsync(clubId);
            return events.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<Models.Event> CreateAsync(Models.Event @event)
        {
            await ValidateReferenceAsync(@event);
            if (@event.Type == EventType.Match && !@event.OpponentClubId.HasValue) throw new ValidationException("A match requires an oppnent club");
            var entity = _toEntityMapper.Map(@event);
            await _eventRepo.AddAsync(entity);
            await _eventRepo.SaveChangesAsync();
            return @event;
        }

        public async Task<Event> UpdateAsync(Models.Event @event)
        {
            var existingEvent = await _eventRepo.GetByIdAsync(@event.Id);
            if (existingEvent is null) throw new NotFoundException($"Event with ID {@event.Id} was not found");
            await ValidateReferenceAsync(@event);
            if (@event.Type == EventType.Match && !@event.OpponentClubId.HasValue) throw new ValidationException("A match requires an apponent club");
            existingEvent.ClubId = @event.ClubId;
            existingEvent.CreatedByUserId = @event.CreatedByUserId;
            existingEvent.Type = @event.Type;
            existingEvent.Description = @event.Description;
            existingEvent.LocationId = @event.LocationId;
            existingEvent.StartTime = @event.StartTime;
            existingEvent.EndTime = @event.EndTime;
            existingEvent.OpponentClubId = @event.OpponentClubId;
            await _eventRepo.UpdateAsync(existingEvent);
            await _eventRepo.SaveChangesAsync();
            return @event;
        }

        public async Task ReqpondAsync(Guid eventId, Guid userId, EventResponseStatus status)
        {
            throw new NotImplementedException();
        }

        private async Task ValidateReferenceAsync(Models.Event @event)
        {
            if (await _clubRepo.GetByIdAsync(@event.ClubId) is null) throw new NotFoundException($"Club with ID {@event.ClubId} not found");
            if (await _locationRepo.GetByIdAsync(@event.LocationId) is null) throw new NotFoundException($"Location with ID {@event.LocationId} not found");
            if (await _userRepo.GetByIdAsync(@event.CreatedByUserId) is null) throw new NotFoundException($"User with ID {@event.CreatedByUserId} not found");
            if (@event.OpponentClubId.HasValue && await _clubRepo.GetByIdAsync(@event.OpponentClubId.Value) is null) throw new NotFoundException($"Opponent club with ID {@event.OpponentClubId} not found");
        }
    }
}