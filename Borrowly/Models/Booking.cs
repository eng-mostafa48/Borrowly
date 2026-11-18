using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RenterId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime? ReturnedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } = null!;
        public User Renter { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Deposit? Deposit { get; set; }
        public Review? Review { get; set; }
    }
}
