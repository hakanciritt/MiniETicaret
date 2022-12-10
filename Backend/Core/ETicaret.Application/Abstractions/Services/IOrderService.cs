using ETicaret.Application.DTOs.Order;
using ETicaret.Application.ViewModels;
using ETicaret.Application.ViewModels.Orders;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrder);
        Task<List<OrderDto>> GetAllOrders(PagedRequest request);
        Task<Order> GetOrderById(string orderId);
    }
}
