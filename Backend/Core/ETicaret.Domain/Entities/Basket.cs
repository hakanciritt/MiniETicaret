using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        //public Guid? OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
