using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BasketItemType? BasketItemType { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
