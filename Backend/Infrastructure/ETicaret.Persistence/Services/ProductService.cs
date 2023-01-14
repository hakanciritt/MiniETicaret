using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Repositories.ProductRepository;
using ETicaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductService(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<Product> GetProduct(string productId)
        {
            var product = await _productReadRepository.DbSet
                .Include(d => d.Categories)
                .Include(d=>d.ProductImageFiles)
                .Include(d => d.TextContent)
                    .ThenInclude(d => d.MetaContent)
                .FirstOrDefaultAsync(d => d.Id == Guid.Parse(productId));

            if (product is null) throw new UserFriendlyException("Ürün bulunamadı");

            return product;
        }
    }
}
