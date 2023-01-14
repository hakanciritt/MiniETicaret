using ETicaret.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public Guid? TextContentId { get; set; }
        [ForeignKey(nameof(TextContentId))] public TextContent? TextContent { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
