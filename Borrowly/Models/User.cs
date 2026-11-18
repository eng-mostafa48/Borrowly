using Borrowly.Enums;

namespace Borrowly.Models
{
    public class User
    {
       
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string PasswordHash { get; set; } = null!;
            public string? Phone { get; set; }
            public UserRole Role { get; set; } = UserRole.User;
            public Language PreferredLanguage { get; set; } = Language.Ar;
            public bool IsActive { get; set; } = true;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public ICollection<Product> Products { get; set; } = new List<Product>();
            public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
            public ICollection<Review> Reviews { get; set; } = new List<Review>();
            public ICollection<Report> Reports { get; set; } = new List<Report>();
            public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        
    }
}
