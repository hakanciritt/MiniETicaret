using ETicaret.Application.Repositories;
using ETicaret.Application.Repositories.BasketItem;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories.BasketItemRepository
{
    public class BasketItemWriteRepository : WriteRepository<BasketItem> , IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
