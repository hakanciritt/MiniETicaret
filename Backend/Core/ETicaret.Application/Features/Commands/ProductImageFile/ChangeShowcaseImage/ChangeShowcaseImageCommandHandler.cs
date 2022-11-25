using ETicaret.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _fileWriteRepository;
        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository fileWriteRepository)
        {
            _fileWriteRepository = fileWriteRepository;
        }
        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _fileWriteRepository.DbSet
                .Include(c => c.Products)
                .SelectMany(c => c.Products,
                (f, p) => new { f, p });

            var queryResult = await query.FirstOrDefaultAsync(d => d.p.Id == request.Id && d.f.IsCoverImage);

            var image = await query.FirstOrDefaultAsync(c => c.f.Id == request.Id);

            if (image is not null)
                image.f.IsCoverImage = true;

            await _fileWriteRepository.SaveAsync();

            return new() { };
        }
    }
}
