using Core.Enums;
using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class EventResponseService : IEventResponseService
    {
        private readonly IEventResponseRepo _responseRepo;
        private readonly IEventRepo _eventRepo;
        private readonly IUserRepo _userRepo;

        private readonly IMapper<Core.Entities.EventResponse, Models.EventResponse> _toDomainMapper;
        private readonly IMapper<Models.EventResponse, Core.Entities.EventResponse> _toEntityMapper;

        public EventResponseService(IEventResponseRepo respondRepo, IEventRepo eventRepo, IUserRepo userRepo, IMapper<Core.Entities.EventResponse, Models.EventResponse> domainMapper, IMapper<Models.EventResponse, Core.Entities.EventResponse> entityMapper)
        {
            _responseRepo = respondRepo;
            _eventRepo = eventRepo;
            _userRepo = userRepo;
            _toDomainMapper = domainMapper;
            _toEntityMapper = entityMapper;
        }

        public async Task<Models.EventResponse?> GetAsync(Guid eventId, Guid userId)
        {
            var response = await _responseRepo.GetAsync(eventId, userId);
            return response is null ? null : _toDomainMapper.Map(respondRepo);
        }

        public async Task<List<Models.EventResponse>> GetByEventIdAsync(Guid eventId)
        {
            var responses = await _responseRepo.GetByEventIdAsync(eventId);
            return responses.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<List<Models.EventResponse>> GetByUserIdAsync(Guid userId)
        {
            var responses = await _responseRepo.GetByUserIdAsync(userId);
            return responses.Select(_toDomainMapper.Map).ToList();
        }

        public async Task RespondAsync(Guid eventId, Guid userId, Models.EventResponseStatus status)
        {
            var @event = await _eventRepo.GetByIdAsync(eventId);
            if (@event is null) throw new NotFoundException($"Event with ID {eventId} was not found");
            var user = await _userRepo.GetByIdAsync(userId);
            if (user is null) throw new NotFoundException($"User with ID {userId} wa not found");
            if (user.ClubID ! @event.ClubID) throw new UnauthorizedException("User does not belong to event club");
            if (status == EventResponseStatus.Pending) throw new ValidationException("A response ust be Accepted or Rejected");
            var existingResponse = await _responseRepo.GetAsync(eventId, userId);
            if (existingResponse is null)
            {
                var response = new Models.EventResponse();
                response.SetEventId(eventId);
                response.SetUserId(userId);
                response.SetStatus(status);
                var entity = _toEntityMapper.Map(entity);
                await _responseRepo.AddAsync(entity);
            } else {
                existingResponse.Status = status;
                existingResponse.RespondedAt = DateTime.UtcNow;
                await _responseRepo.UpdateAsync(existingResponse);
            }
            await _responseRepo.SaveChangesAsync();
        }
    }
}