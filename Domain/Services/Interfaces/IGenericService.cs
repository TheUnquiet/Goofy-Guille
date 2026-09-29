namespace Domain.Services.Interfaces
{
    public interface IGenericService<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        Task<TModel> GetByIdAsync(Guid id);
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
    }
}