using ETicaret.Application.DTOs.Product;

namespace ETicaret.Application.DTOs.Order
{
    public class OrderItemDto : BaseEntityDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public OrderDto Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
