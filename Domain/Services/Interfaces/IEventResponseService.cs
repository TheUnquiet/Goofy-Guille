using Core.Enums;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IEventResponseService
    {
        Task<EventResponse?> GetAsync(Guid eventId, Guid userId);
        Task<List<EventResponse>> GetByEventIdAsync(Guid eventId);
        Task<List<EventResponse>> GetByUserIdAsync(Guid userId);
        Task RespondAsync(Guid eventId, Guid userId, EventResponseStatus status);
    }
}