using ETicaret.Application.Repositories;
using ETicaret.Domain.Entities.Common;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETicaret.Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ETicaretDbContext _context;

        public ReadRepository(ETicaretDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> DbSet => _context.Set<TEntity>();
        public IQueryable<TEntity> GetAll(bool tracking = true)
        {
            var query = DbSet.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var query = DbSet.Where(predicate);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
        {
            var query = DbSet.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        }
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var query = DbSet.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(predicate);

        }
    }
}
