using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
