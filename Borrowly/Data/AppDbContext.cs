using Borrowly.Enums;
using Borrowly.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrowly.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Deposit> Deposits => Set<Deposit>();
        public DbSet<Refund> Refunds => Set<Refund>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void ConfigureConventions(ModelConfigurationBuilder c)
        {
            c.Properties<UserRole>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<Language>().HaveConversion<string>().HaveMaxLength(5);
            c.Properties<ProductStatus>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<BookingStatus>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<PaymentStatus>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<DepositStatus>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<RefundStatus>().HaveConversion<string>().HaveMaxLength(20);
            c.Properties<ReportStatus>().HaveConversion<string>().HaveMaxLength(20);
        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<User>(e =>
            {
                e.Property(x => x.Name).HasMaxLength(100);
                e.Property(x => x.Email).HasMaxLength(256);
                e.Property(x => x.Phone).HasMaxLength(20);
                e.HasIndex(x => x.Email).IsUnique();
            });

            b.Entity<Category>(e =>
            {
                e.Property(x => x.NameEn).HasMaxLength(100);
                e.Property(x => x.NameAr).HasMaxLength(100);
            });

            b.Entity<Product>(e =>
            {
                e.Property(x => x.Title).HasMaxLength(200);
                e.Property(x => x.City).HasMaxLength(100);
                e.Property(x => x.PricePerDay).HasPrecision(10, 2);
                e.Property(x => x.DepositAmount).HasPrecision(10, 2);

                e.HasOne(x => x.Owner).WithMany(u => u.Products)
                    .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Category).WithMany(c => c.Products)
                    .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });

            b.Entity<ProductImage>(e =>
            {
                e.Property(x => x.ImageUrl).HasMaxLength(500);
                e.HasOne(x => x.Product).WithMany(p => p.Images)
                    .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            });

            b.Entity<Booking>(e =>
            {
                e.Property(x => x.TotalPrice).HasPrecision(10, 2);

                e.HasOne(x => x.Product).WithMany(p => p.Bookings)
                    .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Renter).WithMany(u => u.Bookings)
                    .HasForeignKey(x => x.RenterId).OnDelete(DeleteBehavior.Restrict);

             
                e.HasIndex(x => new { x.ProductId, x.StartDate, x.EndDate });

                e.ToTable(t => t.HasCheckConstraint("CK_Bookings_Dates", "[EndDate] > [StartDate]"));
            });

            b.Entity<Payment>(e =>
            {
                e.Property(x => x.Amount).HasPrecision(10, 2);
                e.Property(x => x.PaymobOrderId).HasMaxLength(100);
                e.Property(x => x.PaymobTransactionId).HasMaxLength(100);
                e.HasIndex(x => x.PaymobOrderId).IsUnique();

                e.HasOne(x => x.Booking).WithMany(bk => bk.Payments)
                    .HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            });

            b.Entity<Deposit>(e =>
            {
                e.Property(x => x.Amount).HasPrecision(10, 2);

               
                e.HasOne(x => x.Booking).WithOne(bk => bk.Deposit)
                    .HasForeignKey<Deposit>(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            });

            b.Entity<Refund>(e =>
            {
                e.Property(x => x.Amount).HasPrecision(10, 2);
                e.Property(x => x.Reason).HasMaxLength(300);

                e.HasOne(x => x.Payment).WithMany(p => p.Refunds)
                    .HasForeignKey(x => x.PaymentId).OnDelete(DeleteBehavior.Restrict);
            });

            b.Entity<Review>(e =>
            {
                e.Property(x => x.Comment).HasMaxLength(1000);

                // one review per booking
                e.HasOne(x => x.Booking).WithOne(bk => bk.Review)
                    .HasForeignKey<Review>(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Reviewer).WithMany(u => u.Reviews)
                    .HasForeignKey(x => x.ReviewerId).OnDelete(DeleteBehavior.Restrict);

                e.ToTable(t => t.HasCheckConstraint("CK_Reviews_Rating", "[Rating] BETWEEN 1 AND 5"));
            });

            b.Entity<Report>(e =>
            {
                e.Property(x => x.Reason).HasMaxLength(1000);

                e.HasOne(x => x.Reporter).WithMany(u => u.Reports)
                    .HasForeignKey(x => x.ReporterId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Product).WithMany(p => p.Reports)
                    .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            });

            b.Entity<Notification>(e =>
            {
                e.Property(x => x.Message).HasMaxLength(500);
                e.HasOne(x => x.User).WithMany(u => u.Notifications)
                    .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
