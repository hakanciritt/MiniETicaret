using ETicaret.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ETicaret.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, GetProductImagesQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            //include ile productın dosyaları verilecek

            var product = await _productReadRepository.GetWhere(c=>c.Id == Guid.Parse(request.Id)).FirstOrDefaultAsync();
            return new();
        }
    }
}
