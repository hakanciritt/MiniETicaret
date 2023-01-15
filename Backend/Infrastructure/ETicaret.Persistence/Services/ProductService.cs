using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Repositories;
using ETicaret.Application.Repositories.ProductRepository;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
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
        private readonly IWriteRepository<TextContent> writeRepository;

        public ProductService(IProductReadRepository productReadRepository , IWriteRepository<TextContent> writeRepository)
        {
            _productReadRepository = productReadRepository;
            this.writeRepository = writeRepository;
        }
        public async Task<Product> GetProduct(string productId)
        {
            await writeRepository.AddAsync(new TextContent()
            {
                Description = "<span>açıklama buraya gelecek</span>",
                MetaContent = new MetaContent()
                {
                    MetaKeywords = "keywords , search",
                    Description = "meta açıklama",
                    Title = "başlık"
                },
                Title = "başlık",
                ShortDescription = "kısa açıkalma",
            });

            await writeRepository.SaveAsync();

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
