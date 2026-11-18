namespace Borrowly.Models
{
    public class Category
    {
            public int Id { get; set; }
            public string NameEn { get; set; } = null!;
            public string NameAr { get; set; } = null!;

            public ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}
