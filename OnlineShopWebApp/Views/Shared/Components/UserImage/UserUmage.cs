using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class UserImage : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserImage( UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager?.FindByNameAsync(User?.Identity?.Name)?.Result;

                return View("UserImage", user.ImagePath);
            }

            return View();
        }


    }
}
