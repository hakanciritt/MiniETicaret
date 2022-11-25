using System.Security.Claims;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.ViewModels.Baskets;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Abstractions.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository,IBasketWriteRepository basketWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
        }
        public Task<List<BasketItem>> GetAllBasketItemAsync()
        {

            throw new NotImplementedException();
        }

        public Task AddItemToBasketItemAsync(CreateBasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(UpdateBasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBasketItemAsync(string basketItemId)
        {
            throw new NotImplementedException();
        }

        private async Task<AppUser?> ContextUser()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.Claims?
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.Users.Include(c => c.Baskets).FirstOrDefaultAsync(d => d.Id == userId);
                var userBasket = from basket in user?.Baskets
                                 join order in _orderReadRepository.DbSet
                                     on basket.Id equals order.Id into basketOrders
                                 from ba in basketOrders.DefaultIfEmpty()
                                 select new { Basket = basket, Order = ba };

                Basket? targetBasket = null;
                if (userBasket.Any(d => d.Order is null))
                {
                    targetBasket = userBasket.FirstOrDefault(c => c.Order is null).Basket;
                }
                else
                {
                    user.Baskets.Add(new());

                }
                //_basketWriteRepository.Update( )
            }

            return null;
        }
    }
}
