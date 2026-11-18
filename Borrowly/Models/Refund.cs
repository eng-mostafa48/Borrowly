using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Refund
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
        public RefundStatus Status { get; set; } = RefundStatus.Pending;

        public Payment Payment { get; set; } = null!;
    }
}
