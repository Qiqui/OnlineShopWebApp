namespace OnlineShop.Domain.Entities
{
    public class Compare
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Products { get; }

        public Compare()
        {
            Products = new List<Product>();
        }
    }
}
