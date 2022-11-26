﻿using ETicaret.Application.Abstractions.Services;
using MediatR;

namespace ETicaret.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
    {
        private readonly IBasketService _basketService;
        public UpdateQuantityCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantityAsync(new ViewModels.Baskets.UpdateBasketItem()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });

            return new();

        }
    }
}
