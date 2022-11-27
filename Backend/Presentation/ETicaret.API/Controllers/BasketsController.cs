using ETicaret.Application.Constants;
using ETicaret.Application.Features.Commands.Basket.AddItemToBasket;
using ETicaret.Application.Features.Commands.Basket.RemoveBasketItem;
using ETicaret.Application.Features.Commands.Basket.UpdateQuantity;
using ETicaret.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Schemes.AdminScheme)]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem(RemoveBasketItemCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
