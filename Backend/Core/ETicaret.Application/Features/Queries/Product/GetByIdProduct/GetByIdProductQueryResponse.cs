using ETicaret.Domain.Entities;

namespace ETicaret.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        //public ICollection<Order> Orders { get; set; }

        //public ICollection<ProductImageFile> ProductImageFiles { get; set; }

    }
}
