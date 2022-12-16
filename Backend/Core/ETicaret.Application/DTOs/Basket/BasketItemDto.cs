using ETicaret.Application.DTOs.Product;
using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.Basket
{
    public class BasketItemDto : BaseEntityDto
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BasketItemType? BasketItemType { get; set; }
        public BasketDto Basket { get; set; }
        public ProductDto Product { get; set; }
    }
}
