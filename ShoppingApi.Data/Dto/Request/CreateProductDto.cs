namespace ShoppingApi.Data.Dto.Request
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string[] Colors { get; set; }
        public string Sizes { get; set; }
        public string[] Options { get; set; }
        public string Images { get; set; }
        public int CategoryId { get; set; }
    }
}
