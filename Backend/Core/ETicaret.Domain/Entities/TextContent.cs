using ETicaret.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Domain.Entities
{
    public class TextContent : BaseEntity
    {
        public string? Key { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public Guid? MetaContentId { get; set; }
        [ForeignKey(nameof(MetaContentId))] public MetaContent MetaContent { get; set; }
    }
}
