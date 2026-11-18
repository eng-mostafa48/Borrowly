using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? PaymobOrderId { get; set; }
        public string? PaymobTransactionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Booking Booking { get; set; } = null!;
        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }
}
