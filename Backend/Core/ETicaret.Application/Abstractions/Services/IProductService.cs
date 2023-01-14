using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<Product> GetProduct(string productId);
    }
}
