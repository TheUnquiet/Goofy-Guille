using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface ITeamRepo : IGenericRepo<Team>
    {
        Task<List<Team>> GetByClubIdAsync(Guid clubId);
        Task<List<Team>> GetAllAsync();
    }
}