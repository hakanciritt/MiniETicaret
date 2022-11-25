using ETicaret.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaret.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(150)
                .WithMessage("Maximum 150 karakter uzunluğunda olabilir").MinimumLength(5)
                .WithMessage("Minimum 5 karakter uzunluğunda olabilir.");

            RuleFor(c => c.Stock).NotEmpty().NotNull().WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
                .Must(p => p >= 0).WithMessage("Stok bilgisi negatif bir değer olamaz.");

            RuleFor(c => c.Price).NotEmpty().NotNull().WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
                .Must(p => p >= 0).WithMessage("fiyat bilgisi negatif bir değer olamaz.");
        }
    }
}
