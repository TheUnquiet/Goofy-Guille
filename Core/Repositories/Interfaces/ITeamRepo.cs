using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface ITeamRepo : IGenericRepo<Team>
    {
        Task<List<Team>> GetbyClubIdAsync(Guid clubId);
        Task<List<Team>> GetAllAsync();
    }
}