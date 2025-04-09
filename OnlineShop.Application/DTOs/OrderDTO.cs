using OnlineShop.Application.Enums;

namespace OnlineShop.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int Number { get; set; }
        public ContactInfoDTO ContactInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatusEnum Status { get; set; }
        public List<OrderPositionDTO> Positions { get; set; }
    }
}
