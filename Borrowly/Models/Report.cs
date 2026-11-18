using Borrowly.Enums;

namespace Borrowly.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int ProductId { get; set; }
        public string Reason { get; set; } = null!;
        public ReportStatus Status { get; set; } = ReportStatus.Open;

        public User Reporter { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
