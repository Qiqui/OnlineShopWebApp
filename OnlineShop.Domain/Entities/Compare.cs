namespace OnlineShop.Domain.Entities
{
    public class Compare
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; }

        public Compare()
        {
            Products = new List<Product>();
        }
    }
}
