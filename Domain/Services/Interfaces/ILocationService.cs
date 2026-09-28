using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetByIdAsync(Guid id);
        Task<List<Location>> GetAllAsync();
        Task<Location> CreateAsync(Location location);
        Task<Location> UpdateAsync(Location location);
    }
}