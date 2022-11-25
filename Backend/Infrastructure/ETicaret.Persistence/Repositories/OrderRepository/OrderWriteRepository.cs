using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories.OrderRepository
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
