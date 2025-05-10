namespace OnlineShop.Domain.Entities
{
    public class Comparison
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Products { get; }

        public Comparison()
        {
            Products = new List<Product>();
        }
    }
}
