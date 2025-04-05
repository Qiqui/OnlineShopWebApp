namespace OnlineShop.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<CartPosition> Positions { get; set; }

        public Cart()
        {
            Positions = new List<CartPosition>();
        }
    }
}
