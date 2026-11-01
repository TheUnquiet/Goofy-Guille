using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface IEventRepo : IGenericRepo<Event>
    {
        Task<List<Event>> GetAllAsync();
        Task<List<Event>> GetByClubIdAsync(Guid clubId);
        Task<List<Event>> GetUpcomingByClubIdAsync(Guid clubId);
    }
}