using Core.Repositories.Interfaces;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity>
        where TEntity : class
        where TModel : class
    {
        private readonly IGenericRepo<TEntity> _repo;
        protected readonly IMapper<TEntity, TModel> _toDomainMapper;
        protected readonly IMapper<TModel, TEntity> _toEntityMapper;

        public GenericService(IGenericRepo<TEntity> repo, IMapper<TEntity, TModel> toDomainMapper, IMapper<TModel, TEntity> toEntityMapper)
        {
            _repo = repo;
            _toDomainMapper = toDomainMapper;
            _toEntityMapper = toEntityMapper;
        }

        public async Task<TModel> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) throw new NotFoundException($"{typeof(TModel).Name} with ID {id} was not found");
            return _toDomainMapper.Map(entity);
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            var entity = _toEntityMapper.Map(model);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return model;
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var id = GetId(model);
            var existingEntity = await _repo.GetByIdAsync(id);
            if (existingEntity is null) throw new NotFoundException($"{typeof(TModel).Name} with ID {id} was not found");
            var updatedEntity = _toEntityMapper.Map(model);

            await _repo.UpdateAsync(existingEntity, updatedEntity);
            await _repo.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _repo.GetByIdAsync(id) is null) throw new NotFoundException($"{typeof(TModel).Name} with ID {id} was not found");
            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
        }

        private static Guid GetId(TModel model)
        {
            var porperty = typeof(TModel).GetProperty("Id");
            if (porperty is null) throw new InvalidOperationException($"{typeof(TModel).Name} must have an Id property");
            return (Guid)(porperty.GetValue(model) ?? throw new InvalidOperationException($"{typeof(TModel).Name} has no valid Id."));
        }
    }
}