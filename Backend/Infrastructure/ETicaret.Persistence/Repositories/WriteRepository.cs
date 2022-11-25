using ETicaret.Application.Repositories;
using ETicaret.Domain.Entities.Common;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETicaret.Infrastructure.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ETicaretDbContext _context;

        public WriteRepository(ETicaretDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> DbSet => _context.Set<TEntity>();

        public async Task<bool> AddAsync(TEntity entity)
        {
            EntityEntry result = await DbSet.AddAsync(entity);
            return result.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(TEntity entity)
        {
            EntityEntry result = DbSet.Remove(entity);
            return result.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            return true;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            TEntity find = await DbSet.FirstOrDefaultAsync(c => c.Id == new Guid(id));
            return find != null && Remove(find);
        }

        public bool Update(TEntity entity)
        {
            var result = DbSet.Update(entity);
            return result.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
