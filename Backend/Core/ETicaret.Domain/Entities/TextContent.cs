using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities
{
    public class TextContent : BaseEntity
    {
        public string? Key { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public Guid? MetaContentId { get; set; }
        public MetaContent MetaContent { get; set; }
    }
}
