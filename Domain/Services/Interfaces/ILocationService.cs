using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ILocationService : IGenericService<Location, Core.Entities.Location>
    {
        Task<List<Location>> GetAllAsync();
    }
}