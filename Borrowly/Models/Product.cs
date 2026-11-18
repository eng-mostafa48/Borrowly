using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal DepositAmount { get; set; }
        public string? City { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User Owner { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
