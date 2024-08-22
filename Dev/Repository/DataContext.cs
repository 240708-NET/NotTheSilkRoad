using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Seller> Sellers => Set<Seller>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Category> Categories => Set<Category>();
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product and Category relationship
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategory",
                c => c.HasOne<Category>().WithMany().HasForeignKey("CategoryId").HasPrincipalKey(nameof(Category.Id)),
                p => p.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
                j => j.HasKey("CategoryId", "ProductId")
            );

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("user_type")
            .HasValue<Customer>("Customer")
            .HasValue<Seller>("Seller");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    }
}
