namespace ShoppingApi.Data.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public string[] Colors { get; set; }
    public string Sizes { get; set; }
    public string Options { get; set; }
    public string Images { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
}