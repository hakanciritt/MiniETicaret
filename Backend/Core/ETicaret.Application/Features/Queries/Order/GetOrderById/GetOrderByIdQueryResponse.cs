using ETicaret.Application.DTOs.Order;
using ETicaret.Application.DTOs.User;
using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? BasketId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderNo { get; set; }
        public string? UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal? Discount { get; set; }
        public AppUserDto User { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
