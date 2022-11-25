using ETicaret.Application.Repositories.Basket;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Repositories.BasketRepository
{
    public class BasketReadRepository : ReadRepository<Basket> , IBasketReadRepository
    {
        public BasketReadRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
