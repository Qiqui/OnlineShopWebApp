using OnlineShop.Db.Enum;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Number { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatusEnum Status { get; set; }
        public List<OrderPosition> Positions { get; set; }

        public Order()
        {
            Positions = new List<OrderPosition>();
        }
    }
}