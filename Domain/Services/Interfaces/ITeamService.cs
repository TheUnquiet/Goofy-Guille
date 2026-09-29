using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITeamService : IGenericService<Team, Core.Entities.Team>
    {
        Task<List<Team>> GetByClubIdAsync(Guid clubId);
        Task<List<Team>> GetAllAsync();
    }
}