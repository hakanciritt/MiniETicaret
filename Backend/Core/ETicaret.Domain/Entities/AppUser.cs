using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities
{
    public class AppUser : IdentityUser<string>
    {
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
