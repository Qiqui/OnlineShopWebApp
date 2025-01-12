namespace OnlineShop.Db.Models
{
    public class OrderPosition
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}