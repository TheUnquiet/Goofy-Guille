using Core.Enums;
using Core.Repositories.Interfaces;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class EventService : GenericService<Event, Core.Entities.Event>, IEventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IUserRepo _userRepo;
        private readonly IClubRepo _clubRepo;
        private readonly ILocationRepo _locationRepo;

        public EventService(
            IEventRepo eventRepo, IUserRepo userRepo, 
            IClubRepo clubRepo, ILocationRepo locationRepo, IGenericRepo<Core.Entities.Event> repo,
            IMapper<Core.Entities.Event, Models.Event> domainMapper, 
            IMapper<Models.Event, Core.Entities.Event> entityMapper)
            : base(repo, domainMapper, entityMapper)
        {
            _eventRepo = eventRepo;
            _userRepo = userRepo;
            _clubRepo = clubRepo;
            _locationRepo = locationRepo;
        }

        public async Task<List<Models.Event>> GetAllAsync()
        {
            var events = await _eventRepo.GetAllAsync();
            return events.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<List<Models.Event>> GetByClubIdAsync(Guid clubId)
        {
            var events = await _eventRepo.GetByClubIdAsync(clubId);
            return events.Select(_toDomainMapper.Map).ToList();
        }

        public override async Task<Models.Event> CreateAsync(Models.Event @event)
        {
            await ValidateReferenceAsync(@event);
            if (@event.Type == EventType.Match && !@event.OpponentClubId.HasValue) throw new ValidationException("A match requires an opponent club");
            return await base.CreateAsync(@event);
        }

        public override async Task<Models.Event> UpdateAsync(Models.Event @event)
        {
            var existingEvent = await _eventRepo.GetByIdAsync(@event.Id);
            if (existingEvent is null) throw new NotFoundException($"Event with ID {@event.Id} was not found");
            @event.SetCreatedByUser(existingEvent.CreatedByUserId);
            await ValidateReferenceAsync(@event);
            if (@event.Type == EventType.Match && !@event.OpponentClubId.HasValue) throw new ValidationException("A match requires an opponent club");
            existingEvent.ClubId = @event.ClubId;
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

        private async Task ValidateReferenceAsync(Models.Event @event)
        {
            if (await _clubRepo.GetByIdAsync(@event.ClubId) is null) throw new NotFoundException($"Club with ID {@event.ClubId} not found");
            if (await _locationRepo.GetByIdAsync(@event.LocationId) is null) throw new NotFoundException($"Location with ID {@event.LocationId} not found");
            if (await _userRepo.GetByIdAsync(@event.CreatedByUserId) is null) throw new NotFoundException($"User with ID {@event.CreatedByUserId} not found");
            if (@event.OpponentClubId.HasValue && await _clubRepo.GetByIdAsync(@event.OpponentClubId.Value) is null) throw new NotFoundException($"Opponent club with ID {@event.OpponentClubId} not found");
        }
    }
}