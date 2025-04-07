using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Identity;

namespace OnlineShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> GetCurrentUserIdAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new NotFoundException("Не удалось идентефицировать пользователя");

            return user.Id;
        }
    }
}
