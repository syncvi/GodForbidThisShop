using Microsoft.EntityFrameworkCore;
using ShoppingAttire.Models;

namespace ShoppingAttire.Data
{
    public class WorkingContext : DbContext
    {
        public WorkingContext(DbContextOptions<WorkingContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingAttire.Models.Product> Product { get; set; }

        public DbSet<ShoppingAttire.Models.Category> Category { get; set; }

        public DbSet<ShoppingAttire.Models.CategoryProduct> CategoryProduct { get; set; }

        public DbSet<ShoppingAttire.Models.Producer> Producer { get; set; }

        public DbSet<ShoppingAttire.Models.Role> Role { get; set; }

        public DbSet<ShoppingAttire.Models.SiteUser> SiteUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creating tables in the DB and setting primary keys and foreign keys
            //https://youtu.be/7M501P-23Jg
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasOne(p => p.Producer).WithMany(px => px.Products).HasForeignKey(p => p.ProducerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Role>().ToTable("Role");
            //modelBuilder.Entity<RoleUser>().ToTable("RoleUser");
            modelBuilder.Entity<SiteUser>().ToTable("User");
            modelBuilder.Entity<Producer>().ToTable("Producer");
            modelBuilder.Entity<CategoryProduct>().ToTable("CategoryProduct");
            modelBuilder.Entity<CategoryProduct>().HasKey(cp_pk => new { cp_pk.ProductId, cp_pk.CategoryId });
            modelBuilder.Entity<CategoryProduct>().HasOne(cp_c => cp_c.Product)
                .WithMany(cp_p => cp_p.Categories)
                .HasForeignKey(c_p_connection => c_p_connection.ProductId).OnDelete(DeleteBehavior.Cascade);
            //here we do: one product has many categoires
            modelBuilder.Entity<CategoryProduct>().HasOne(cp_p => cp_p.Category)
                .WithMany(cp_c => cp_c.Products)
                .HasForeignKey(p_c_connection => p_c_connection.CategoryId).OnDelete(DeleteBehavior.Cascade);
            //here we do: one category has many products

            modelBuilder.Entity<SiteUser>().HasData(new Models.SiteUser
            {
                UserId = 1,
                UserName = "Admin",
                Password = "e604fb2072c286d1fb6378c5cde74ca0c99f3ba1d9f4cef58969020efbc2382e",
                Role = "Admin"
            });

            modelBuilder.Entity<Role>().HasData(new Models.Role
            {
                Id = 1,
                RoleName = "Admin"
            },
            new Models.Role
            {
                Id = 2,
                RoleName = "Sales"
            },
            new Models.Role
            {
                Id = 3,
                RoleName = "Customer"
            });

            modelBuilder.Entity<Producer>().HasData(new Models.Producer
            {
                Id = 1,
                ProducerName = "Bruh"
            },
            new Models.Producer
            {
                Id = 2,
                ProducerName = "1738"
            }
            );

        }

    }
}
