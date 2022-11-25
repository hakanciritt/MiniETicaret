using ETicaret.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository )
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int totalProductCount = await _productReadRepository.GetAll(false).CountAsync();

            var products = await _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
                .Select(d => new
                {
                    Id = d.Id,
                    Name = d.Name,
                    Stock = d.Stock,
                    Price = d.Price,
                    CreatedDate = d.CreateData,
                    UpdatedDate = d.UpdatedDate
                }).ToListAsync();
            return new() { Products = products, TotalCount = totalProductCount };
        }
    }
}
