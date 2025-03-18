using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TranDinhDuong_2280600533.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed cho Category (Id kiểu int)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "PC" },
                new Category { Id = 3, Name = "Phone" },
                new Category { Id = 4, Name = "Other" }
            );

            // Seed cho IdentityRole (Id kiểu string)
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "4b7a7a8a-6bba-76fd-1684-abc123abc123",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "7e0ab912-9508-48c2-83e6-8313d3c0d504",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "8313d3c0-dd50-4fac-b9be-7777cccc1111",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = "8318afc0-9b24-4600-b520-4444bbbb2222",
                    Name = "Company",
                    NormalizedName = "COMPANY"
                }
            );
        }
    }
}