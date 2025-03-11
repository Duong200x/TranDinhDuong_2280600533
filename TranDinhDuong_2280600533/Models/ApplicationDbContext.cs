using Microsoft.EntityFrameworkCore;

namespace TranDinhDuong_2280600533.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "PC" },
                new Category { Id = 3, Name = "Phone" },
                new Category { Id = 4, Name = "Other" }
                );
        }
    }
}
