using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.BasketItem;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories.BasketItemRepository
{
    public class BasketItemReadRepository : ReadRepository<BasketItem> , IBasketItemReadRepository
    {
        public BasketItemReadRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
