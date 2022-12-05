using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.UserSession;
using ETicaret.Application.ViewModels.Orders;
using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IUserSession _userSession;

        public OrderService(IOrderWriteRepository orderWriteRepository,
            IBasketReadRepository basketReadRepository,
            IUserSession userSession)
        {
            _orderWriteRepository = orderWriteRepository;
            _basketReadRepository = basketReadRepository;
            _userSession = userSession;
        }
        public async Task CreateOrder(CreateOrderDto createOrder)
        {
            var userBasket = await _basketReadRepository.DbSet.Include(d => d.BasketItems)
                    .FirstOrDefaultAsync(d =>
                        d.BasketStatus == Domain.Enums.Status.Active && d.UserId == _userSession.UserId);

            if (userBasket == null) throw new UserFriendlyException("Kullanıcı sepeti bulunamadı.");
            
            if (_userSession.GetUserId != userBasket.UserId) throw new UserFriendlyException("Bu sepet üzerinde işlem yapamazsınız");

            var order = new Domain.Entities.Order()
            {
                Address = createOrder.Address,
                Description = createOrder.Description,
                BasketId = userBasket.Id,
                UserId = _userSession.GetUserId,
                OrderNo = new Random().Next(100000, 999999).ToString(),
                TotalPrice = userBasket.BasketItems.Sum(c => c.Quantity * c.Price),
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in userBasket.BasketItems.ToList())
            {
                order.OrderItems.Add(new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                });
            }

            await _orderWriteRepository.AddAsync(order);

            userBasket.BasketStatus = Domain.Enums.Status.Passive;
            await _orderWriteRepository.SaveAsync();
        }
    }
}
