using ETicaret.Application.ViewModels.Baskets;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetAllBasketItemAsync();
        Task AddItemToBasketItemAsync(CreateBasketItem basketItem);
        Task UpdateQuantityAsync(UpdateBasketItem basketItem);
        Task RemoveBasketItemAsync(string basketItemId);
    }
}
