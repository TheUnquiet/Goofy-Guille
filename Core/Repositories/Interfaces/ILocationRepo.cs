using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface ILocationRepo : IGenericRepo
    {
        Task<List<Location>> GetAllAsync();
    }
}