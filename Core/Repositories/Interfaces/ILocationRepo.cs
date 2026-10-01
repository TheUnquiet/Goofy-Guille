using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface ILocationRepo : IGenericRepo<Location>
    {
        Task<List<Location>> GetAllAsync();
    }
}