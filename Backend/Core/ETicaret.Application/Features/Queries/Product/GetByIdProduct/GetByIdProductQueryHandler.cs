using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Repositories.ProductRepository;
using MediatR;

namespace ETicaret.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductService _productService;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository , IProductService productService )
        {
            _productReadRepository = productReadRepository;
            _productService = productService;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);
          
            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
