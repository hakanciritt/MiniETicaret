﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.Repositories.ProductRepository;
using MediatR;

namespace ETicaret.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });

            await _productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");

            return new();
        }
    }
}
