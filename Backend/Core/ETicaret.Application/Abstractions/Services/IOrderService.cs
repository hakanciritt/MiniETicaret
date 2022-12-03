using ETicaret.Application.ViewModels.Orders;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrder);
    }
}
