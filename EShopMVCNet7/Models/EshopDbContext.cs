﻿using Microsoft.EntityFrameworkCore;

namespace EShopMVCNet7.Models
{
    public class EshopDbContext : DbContext
    {
        public DbSet<AppCategory> AppCategories { get; set; }
        public DbSet<AppOrder> AppOrders { get; set; }
        public DbSet<AppOrderDetal> AppOrderDetals { get; set; }
        public DbSet<AppProduct> AppProducts { get; set; }
        public DbSet<AppProductImage> AppProductImages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppEvens> AppEvens { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connection = "Server=DESKTOP-9SAS8HE\\VITAMINCO;Database=RazorPage;Trusted_Connection=True;Encrypt=False";
        //    optionsBuilder.UseSqlServer(connection);
        //}

        //sữa đổi độ dài của bảng AppCategory
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //bảng AppCategory
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Name)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Slug)
                        .HasMaxLength(200);

            //bảng AppProduct
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Name)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Slug)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Summary)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Content)
                        .HasMaxLength(1000);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.CoverImg)
                        .HasMaxLength(1000);
            //Cấu hình khóa ngoại
            modelBuilder.Entity<AppProduct>()
                        .HasOne(m => m.Category) //bảng AppProduct
                        .WithMany(m => m.Products) //bảng Category
                        .HasForeignKey(m => m.CategoryId) //khóa ngoại
                        .OnDelete(DeleteBehavior.NoAction);

            //
/*            modelBuilder.Entity<AppProduct>()
                        .HasOne(m => m.Even) //bảng AppProduct
                        .WithOne(m => m.EvenProducts) //bảng AppEven
                        .HasForeignKey(m => m.EvenId) //khóa ngoại
                        .OnDelete(DeleteBehavior.NoAction);*/

            //Bảng AppProductImage
            modelBuilder.Entity<AppProductImage>()
                        .Property(i => i.Path)
                        .HasMaxLength(300);
            //Cấu hình khóa ngoại
            modelBuilder.Entity<AppProductImage>()
                        .HasOne(i => i.Product) //bảng AppProduct
                        .WithMany(i => i.ProductImages) //bảng ProductImage
                        .HasForeignKey(i => i.ProductId); //khóa ngoại

            //Bảng AppOrder
            modelBuilder.Entity<AppOrder>()
                .Property(m => m.CustomerAddress)
                .HasMaxLength(500);
            modelBuilder.Entity<AppOrder>()
               .Property(m => m.CustomerEmail)
               .HasMaxLength(100);
            modelBuilder.Entity<AppOrder>()
               .Property(m => m.CustomerName)
               .HasMaxLength(100);
            modelBuilder.Entity<AppOrder>()
               .Property(m => m.CustomerPhone)
               .HasMaxLength(20);
            //khóa ngoại
            modelBuilder.Entity<AppOrder>()
                .HasMany(m => m.Detail)
                .WithOne(m => m.AppOrder)
                .HasForeignKey(m => m.OrderId);
            //Bảng AppEvents
            modelBuilder.Entity<AppEvens>()
                        .Property(m => m.NameEven)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppEvens>()
                        .Property(m => m.ContentEven)
                        .HasMaxLength(1000);
            modelBuilder.Entity<AppEvens>()
                       .Property(m => m.CoverImgEven)
                       .HasMaxLength(1000);
        }
    }
}
