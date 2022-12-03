using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.Abstractions.Services;
using MediatR;

namespace ETicaret.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandRequestHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderHubService _orderHubService;

        public CreateOrderCommandRequestHandler(IOrderService orderService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _orderHubService = orderHubService;
        }
        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrder(new ViewModels.Orders.CreateOrderDto()
            {
                Address = request.Address,
                Description = request.Description,
            });

            await _orderHubService.OrderAddedMessageAsync("Heyy, yeni bir siparişimiz var.");
            return new();
        }
    }
}
