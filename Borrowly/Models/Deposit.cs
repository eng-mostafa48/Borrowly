using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DepositStatus Status { get; set; } = DepositStatus.Held;

        public Booking Booking { get; set; } = null!;
    }
}
