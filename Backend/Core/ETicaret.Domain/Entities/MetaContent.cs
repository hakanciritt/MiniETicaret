using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities
{
    public class MetaContent :BaseEntity
    {
        public string? MetaKeywords { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
