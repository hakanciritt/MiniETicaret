using ETicaret.Application.DTOs.Basket;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.DTOs.Product
{
    public class ProductDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}
