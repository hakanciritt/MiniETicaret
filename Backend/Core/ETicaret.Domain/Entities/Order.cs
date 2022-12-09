using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid? BasketId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderNo { get; set; }
        public string? UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public AppUser User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal? Discount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
