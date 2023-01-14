using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public Status Status { get; set; }
        public Guid? MainCategoryId { get; set; }
        [ForeignKey(nameof(MainCategoryId))] public Category? MainCategory { get; set; }
        public Guid? TextContentId { get; set; }
        [ForeignKey(nameof(TextContentId))] public TextContent? TextContent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
