using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IEventService : IGenericService<Event, Core.Entities.Event> 
    {
        Task<List<Event>> GetAllAsync();
        Task<List<Event>> GetByClubIdAsync(Guid id);
    }
}