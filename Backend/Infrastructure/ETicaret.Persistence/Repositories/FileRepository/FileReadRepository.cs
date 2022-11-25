
using ETicaret.Application.Repositories;
using ETicaret.Persistence.Contexts;
using File = ETicaret.Domain.Entities.File;

namespace ETicaret.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
