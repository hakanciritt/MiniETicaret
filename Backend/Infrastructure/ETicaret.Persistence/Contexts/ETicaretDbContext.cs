using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using File = ETicaret.Domain.Entities.File;

namespace ETicaret.Persistence.Contexts
{
    public class ETicaretDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETicaretDbContext(DbContextOptions<ETicaretDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(d => d.Id);
            builder.Entity<Basket>().HasKey(d => d.Id);

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in entities)
            {
                var entity = (BaseEntity)data.Entity;

                switch (data.State)
                {
                    case EntityState.Added:
                        entity.CreateData = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
