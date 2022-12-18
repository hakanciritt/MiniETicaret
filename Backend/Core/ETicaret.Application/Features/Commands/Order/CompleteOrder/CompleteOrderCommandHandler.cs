using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.Abstractions.Services;
using MediatR;

namespace ETicaret.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderHubService _orderHubService;

        public CompleteOrderCommandHandler(IOrderService orderService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _orderHubService = orderHubService;
        }
        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.Id);

            await _orderHubService.OrderAddedMessageAsync("Heyy, Yeni bir siparişimiz var");
            return new();
        }
    }
}
