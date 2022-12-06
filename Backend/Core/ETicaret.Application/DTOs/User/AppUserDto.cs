using ETicaret.Application.DTOs.Basket;
using ETicaret.Application.DTOs.Order;

namespace ETicaret.Application.DTOs.User
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public ICollection<BasketDto> Baskets { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
    }
}
