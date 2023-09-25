using Microsoft.EntityFrameworkCore;

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
                        .HasForeignKey(m => m.CategoryId); //khóa ngoại
        }
    }
}
