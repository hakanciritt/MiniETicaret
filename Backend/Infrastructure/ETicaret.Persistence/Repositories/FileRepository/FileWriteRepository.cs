using ETicaret.Application.Repositories;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;
using File = ETicaret.Domain.Entities.File;

namespace ETicaret.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
