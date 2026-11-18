namespace Borrowly.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ReviewerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public Booking Booking { get; set; } = null!;
        public User Reviewer { get; set; } = null!;
    }
}
