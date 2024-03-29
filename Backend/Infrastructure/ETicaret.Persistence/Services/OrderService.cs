﻿using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.DTOs.Order;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Helpers;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.UserSession;
using ETicaret.Application.ViewModels;
using ETicaret.Application.ViewModels.Orders;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IUserSession _userSession;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository,
            IBasketReadRepository basketReadRepository,
            IUserSession userSession,
            IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _basketReadRepository = basketReadRepository;
            _userSession = userSession;
            _orderReadRepository = orderReadRepository;
        }

        public async Task<Order> CreateOrder(CreateOrderDto createOrder)
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
                OrderStatus = Domain.Enums.OrderStatus.Created,
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

            //sepet kilitlendi 
            //todo daha global bir yerden yönetilebilir.
            userBasket.Lock = true;
            await _orderWriteRepository.SaveAsync();

            return order;
        }

        public async Task<List<OrderDto>> GetAllOrders(PagedRequest request)
        {
            //todo: mapleme işlemleri için automapper kurulacak.

            return await _orderReadRepository.DbSet
                .Include(c => c.User)
                 .Include(d => d.OrderItems)
                 .PageBy(request.Page, request.Size)
                 .OrderByDescending(c => c.CreateData)
                 .Select(d => new OrderDto()
                 {
                     Id = d.Id,
                     CreateData = d.CreateData,
                     OrderNo = d.OrderNo,
                     OrderStatus = d.OrderStatus,
                     TotalPrice = d.TotalPrice,
                     Description = d.Description,
                     User = new Application.DTOs.User.AppUserDto()
                     {
                         PhoneNumber = d.User.PhoneNumber,
                         UserName = d.User.UserName
                     }
                 }).ToListAsync();
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            var order = await _orderReadRepository.DbSet
                .Include(c => c.OrderItems).ThenInclude(c => c.Product)
                .Include(d => d.User)
                .FirstOrDefaultAsync(c => c.Id == Guid.Parse(orderId));
            if (order is null) throw new UserFriendlyException("Sipariş bulunamadı.");
            return order;
        }
        public async Task CompleteOrderAsync(string id)
        {
            var order = await _orderReadRepository.Query()
                .Include(c => c.OrderItems)
                .Where(c => c.Id == Guid.Parse(id)).FirstOrDefaultAsync();

            if (order is null) throw new UserFriendlyException("Sipariş bulunamadı.");

            order.OrderStatus = Domain.Enums.OrderStatus.Completed;
            var basket = await _basketReadRepository.GetByIdAsync(order.BasketId.Value.ToString());
            basket.BasketStatus = Domain.Enums.Status.Passive;
            basket.Lock = true;

            await _orderWriteRepository.SaveAsync();
        }
    }
}
