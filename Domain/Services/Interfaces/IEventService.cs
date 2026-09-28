using Core.Entities;
using Core.Enums;

namespace Domain.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> GetByIdAsync(Guid id);
        Task<List<Event>> GetByClubIdAsync(Guid id);
        Task<Event> CreateAsync(Event @event);
        Task<Event> UpdateAsync(Event @event);
    }
}