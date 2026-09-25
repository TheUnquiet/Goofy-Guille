using Core.Entities;

namespace Domain.Services.Interfaces
{
    public interface IClubService
    {
        Task<Club> GetByIdAsync(Guid id);
        Task<List<Club>> GetAllAsync();
        Task<Club> CreateAsync(Club club);
        Task<Club> UpdateAsync(Club club);
    }
}