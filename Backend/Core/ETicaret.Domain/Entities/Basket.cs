using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public Status BasketStatus { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
