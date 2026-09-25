using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface IEventResponseRepo
    {
        Task<EventResponse> GetAsync(Guid eventId, Guid userId);
        Task<List<EventResponse>> GetByEventIdAsync(Guid eventId);
        Task<List<EventResponse>> GetByUserIdAsync(Guid userId);
    }
}