using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    { 
        public Guid DomainUserId { get; set; }
        public User? DomainUser { get; set; } 
    }
}
