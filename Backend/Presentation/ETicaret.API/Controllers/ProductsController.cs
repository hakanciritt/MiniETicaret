using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.Constants;
using ETicaret.Application.Features.Commands.Product.CreateProduct;
using ETicaret.Application.Features.Commands.Product.RemoveProduct;
using ETicaret.Application.Features.Commands.Product.UpdateProduct;
using ETicaret.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ETicaret.Application.Features.Queries.Product.GetAllProduct;
using ETicaret.Application.Features.Queries.Product.GetByIdProduct;
using ETicaret.Application.Features.Queries.ProductImageFile.GetProductImages;
using ETicaret.Application.Repositories;
using ETicaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Schemes.AdminScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductImageFileWriteRepository _productImageWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IMediator _mediator;

        public ProductsController(
            IProductImageFileWriteRepository productImageWriteRepository,
            IStorageService storageService,
            IMediator mediator)
        {
            _productImageWriteRepository = productImageWriteRepository;
            _storageService = storageService;
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            //image id değerini alıyoruz ama biz azure blog entegrasyonu yapmadığımız için sadece yazmış olduk 
            //request.ImageId = imageId;
            var result = await _mediator.Send(request);
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var data = await _storageService.UploadAsync("resource/files", Request.Form.Files);

            await _productImageWriteRepository.AddRangeAsync(
                data.Select(c => new ProductImageFile()
                {
                    FileName = c.fileName,
                    Path = c.pathOrContainerName,
                    Storage = _storageService.StorageName
                }).ToList());

            await _productImageWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = Schemes.AdminScheme)]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}