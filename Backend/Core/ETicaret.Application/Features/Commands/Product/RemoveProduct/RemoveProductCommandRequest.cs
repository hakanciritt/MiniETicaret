using MediatR;

namespace ETicaret.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
