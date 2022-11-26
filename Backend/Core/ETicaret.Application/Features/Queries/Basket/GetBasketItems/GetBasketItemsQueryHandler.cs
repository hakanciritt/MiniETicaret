using ETicaret.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetAllBasketItemAsync();


            return basketItems.Select(d=>new GetBasketItemsQueryResponse()
            {
                BasketItemId = d.Id.ToString(),
                Name = d.Product.Name,
                Price = d.Product.Price ,
                Quantity = d.Quantity
            }).ToList();
        }
    }
}
