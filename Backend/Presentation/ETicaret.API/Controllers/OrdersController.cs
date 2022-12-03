using ETicaret.Application.Features.Commands.Order.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }


    }
}
