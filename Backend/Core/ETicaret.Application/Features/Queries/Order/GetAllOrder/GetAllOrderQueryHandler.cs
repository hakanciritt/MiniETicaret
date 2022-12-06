using ETicaret.Application.Abstractions.Services;
using MediatR;

namespace ETicaret.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, GetAllOrderQueryResponse>
    {
        private readonly IOrderService _orderService;
        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrders(new ViewModels.PagedRequest { Page = request.Page, Size = request.Size });

            var response = orders.Select(c => new OrderResponseModel()
            {
                CreatedDate = c.CreateData,
                OrderNo = c.OrderNo,
                TotalPrice = c.TotalPrice,
                UserName = c.User.UserName
            }).ToList();

            return new GetAllOrderQueryResponse
            {
                Orders = response,
                TotalOrderCount = response.Count
            };
        }
    }
}
