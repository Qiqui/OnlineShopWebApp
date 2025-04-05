using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Domain.Entities
{
    public class User : IdentityUser
    {
        [ProtectedPersonalData]
        public string? Name { get; set; }
        [ProtectedPersonalData]
        public string? Surname { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}

