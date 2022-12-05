using System.Security.Claims;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.BasketItem;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.Repositories.ProductRepository;
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
        private readonly IProductReadRepository _productReadRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager,
            IOrderReadRepository orderReadRepository,
            IBasketWriteRepository basketWriteRepository,
            IBasketReadRepository basketReadRepository,
            IBasketItemReadRepository basketItemReadRepository,
            IBasketItemWriteRepository basketItemWriteRepository,
            IUserSession userSession, IProductReadRepository productReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _userSession = userSession;
            _productReadRepository = productReadRepository;
        }
        public async Task<List<BasketItem>> GetAllBasketItemAsync()
        {
            bool checkBasket = await _basketReadRepository.DbSet.AnyAsync(c =>
                c.UserId == _userSession.GetUserId && c.BasketStatus == Domain.Enums.Status.Active);

            if (checkBasket)
            {
                var userBasket = await _basketReadRepository.DbSet.
                    Include(d => d.BasketItems)
                    .ThenInclude(d => d.Product)
                    .FirstOrDefaultAsync(d => d.UserId == _userSession.GetUserId && d.BasketStatus == Domain.Enums.Status.Active);

                return userBasket.BasketItems.ToList();
            }

            Basket targetBasket = new()
            {
                BasketStatus = Domain.Enums.Status.Active,
                UserId = _userSession.GetUserId,
                BasketItems = new List<BasketItem>()
            };

            await _basketWriteRepository.AddAsync(targetBasket);
            await _basketWriteRepository.SaveAsync();
            return targetBasket.BasketItems.ToList();
        }

        public async Task AddItemToBasketItemAsync(CreateBasketItem basketItem)
        {
            var basket = await GetBasketForUser();

            if (basket != null)
            {
                var items = basket.BasketItems.ToList();
                var product = await _productReadRepository.GetByIdAsync(basketItem.ProductId.ToString());

                if (product is null) throw new UserFriendlyException("Ürün bulunamadı.");

                var productIsInBasket = items.FirstOrDefault(d => d.ProductId == basketItem.ProductId);
                if (productIsInBasket != null) productIsInBasket.Quantity++;
                else
                {
                    basket.BasketItems.Add(new()
                    {
                        BasketId = basket.Id,
                        ProductId = basketItem.ProductId,
                        Quantity = basketItem.Quantity,
                        Price = product.Price
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

        private async Task<Basket?> GetBasketForUser()
        {
            string userId = _userSession.GetUserId;

            if (!string.IsNullOrEmpty(userId))
            {
                var userBasket = await _basketReadRepository.DbSet.Include(c => c.BasketItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.BasketStatus == Domain.Enums.Status.Active);

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
