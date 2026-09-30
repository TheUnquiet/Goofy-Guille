using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IClubService : IGenericService<Club, Core.Entities.Club>
    {
        Task<List<Club>> GetAllAsync();
    }
}