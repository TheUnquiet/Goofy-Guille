using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Team> GetByIdAsync(Guid id);
        Task<List<Team>> GetByClubIdAsync(Guid clubId);
        Task<List<Team>> GetAllAsync();
        Task<Team> CreateAsync();
        Task<Team> UpdateAsync();
        Task DeleteAsync(Guid id);
    }
}