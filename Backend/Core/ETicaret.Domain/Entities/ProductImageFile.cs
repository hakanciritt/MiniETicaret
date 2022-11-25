namespace ETicaret.Domain.Entities
{
    public class ProductImageFile : File
    {
        public bool IsCoverImage { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
