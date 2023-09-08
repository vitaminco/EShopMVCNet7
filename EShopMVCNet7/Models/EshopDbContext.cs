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

        public EshopDbContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connection = "Server=DESKTOP-9SAS8HE\\VITAMINCO;Database=RazorPage;Trusted_Connection=True;Encrypt=False";
        //    optionsBuilder.UseSqlServer(connection);
        //}
    }
}
