using ETicaret.Application.Repositories;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories.BasketRepository
{
    public class BasketWriteRepository : WriteRepository<Basket> , IBasketWriteRepository
    {
        public BasketWriteRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
