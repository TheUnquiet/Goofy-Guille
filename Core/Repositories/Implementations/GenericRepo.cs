using Core.Context;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        protected readonly BasketBallClubDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepo(BasketBallClubDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await dbSet.AddAsync(entity);
            return entry.Entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity existingEntity, TEntity updatedEntity)
        {
            context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            return Task.FromResult(existingEntity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null) dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}