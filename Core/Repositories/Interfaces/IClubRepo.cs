using Core.Entities;

namespace Core.Repositories.Interfaces
{
    public interface IClubRepo : IGenericRepo<Club>
    {
        Task<List<Club>> GetAllAsync();
    }
}