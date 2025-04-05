namespace OnlineShop.Domain.Entities
{
    public class ContactInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsAgreeWithDataProcessing { get; set; }
        public List<Order> Orders { get; set; }
        
        public ContactInfo()
        {
            Orders = new List<Order>();
        }
    }
}
