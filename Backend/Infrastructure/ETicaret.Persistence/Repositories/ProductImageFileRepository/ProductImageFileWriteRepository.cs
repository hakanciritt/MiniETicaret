using ETicaret.Application.Repositories;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
