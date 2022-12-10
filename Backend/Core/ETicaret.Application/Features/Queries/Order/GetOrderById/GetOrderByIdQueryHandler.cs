using ETicaret.Application.Abstractions.Services;
using MediatR;

namespace ETicaret.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderById(request.Id);

            return new GetOrderByIdQueryResponse()
            {
                Id = order.Id,
                Address = order.Address,
                CreatedDate = order.CreateData,
                OrderNo = order.OrderNo,
                OrderStatus = order.OrderStatus,
                TotalPrice = order.TotalPrice,
                User = new DTOs.User.AppUserDto()
                {
                    Email = order.User.Email,
                    NameSurname = order.User.NameSurname
                },
                OrderItems = order.OrderItems.Select(c => new DTOs.Order.OrderItemDto()
                {
                    Product = new DTOs.Product.ProductDto()
                    {
                        Name = c.Product.Name
                    },
                    Quantity = c.Quantity,
                    Discount = c.Discount,
                    Price = c.Price,
                }).ToList()
            };

        }
    }
}