using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Data.Models
{
    public class Brand
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
