namespace MyApiNetCore6.Models.Service
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

    }
}
