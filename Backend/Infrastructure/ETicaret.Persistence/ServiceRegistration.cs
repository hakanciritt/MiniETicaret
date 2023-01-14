using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Abstractions.Services.Authentications;
using ETicaret.Application.Repositories;
using ETicaret.Application.Repositories.Basket;
using ETicaret.Application.Repositories.BasketItem;
using ETicaret.Application.Repositories.OrderRepository;
using ETicaret.Application.Repositories.ProductRepository;
using ETicaret.Domain.Entities;
using ETicaret.Infrastructure.Repositories;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories;
using ETicaret.Persistence.Repositories.BasketItemRepository;
using ETicaret.Persistence.Repositories.BasketRepository;
using ETicaret.Persistence.Repositories.OrderRepository;
using ETicaret.Persistence.Repositories.ProductRepository;
using ETicaret.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                }).AddEntityFrameworkStores<ETicaretDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultTokenProviders();

        }
    }
}
