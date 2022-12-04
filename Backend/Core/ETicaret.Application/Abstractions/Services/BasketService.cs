using System.Security.Claims;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.BasketItem;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.UserSession;
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
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IUserSession _userSession;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager,
            IOrderReadRepository orderReadRepository,
            IBasketWriteRepository basketWriteRepository,
            IBasketReadRepository basketReadRepository,
            IBasketItemReadRepository basketItemReadRepository,
            IBasketItemWriteRepository basketItemWriteRepository,
            IUserSession userSession)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _userSession = userSession;
        }
        public async Task<List<BasketItem>> GetAllBasketItemAsync()
        {
            var basket = await ContextUser();

            var userBasket = await _basketReadRepository.DbSet.
                Include(d => d.BasketItems)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == basket.Id);

            return userBasket.BasketItems.ToList();

        }

        public async Task AddItemToBasketItemAsync(CreateBasketItem basketItem)
        {
            var basket = await ContextUser();

            if (basket != null)
            {
                BasketItem? checkProduct = await _basketItemReadRepository.GetSingleAsync(d =>
                    d.BasketId == basket.Id && d.ProductId == basketItem.ProductId);

                if (checkProduct is not null)
                {
                    checkProduct.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = basketItem.ProductId,
                        Quantity = basketItem.Quantity

                    });
                }
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItem basketItem)
        {
            BasketItem? item = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (item != null)
            {
                item.Quantity = basketItem?.Quantity ?? 0;
                await _basketItemWriteRepository.SaveAsync();
            }

        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        private async Task<Basket?> ContextUser()
        {
            string userId = _userSession.GetUserId;

            if (!string.IsNullOrEmpty(userId))
            {
                var userBasket = await _basketReadRepository.DbSet.FirstOrDefaultAsync(c => c.UserId == userId && c.BasketStatus == Domain.Enums.Status.Active);

                if (userBasket is not null) return userBasket;
                
                Basket targetBasket = new()
                {
                    BasketStatus = Domain.Enums.Status.Active,
                    UserId = userId,
                };

                await _basketWriteRepository.AddAsync(targetBasket);
                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }

            throw new UserFriendlyException("kullanıcı bulunamadı.");
        }
    }
}
