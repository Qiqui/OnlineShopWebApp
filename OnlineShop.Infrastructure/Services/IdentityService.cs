using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Common;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Identity;

namespace OnlineShop.Infrastructure.Services
{
    internal sealed class IdentityService : IIdentityService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepositoy;


        public Task<Result> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> RegisterAsync(string email, string password, string fullName)
        {
            throw new NotImplementedException();
        }
    }
}
