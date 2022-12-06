using ETicaret.Application.DTOs.User;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Application.DTOs.Order
{
    public class OrderDto : BaseEntityDto
    {
        public Guid? BasketId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderNo { get; set; }
        public string? UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public AppUserDto User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
