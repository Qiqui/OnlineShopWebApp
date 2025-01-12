using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class Cart : ViewComponent
    {
        private readonly ICartsRepository _cartRepository;
        private readonly UserManager<User> _userManager;

        public Cart(ICartsRepository cartRepository, UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager?.FindByNameAsync(User?.Identity?.Name)?.Result;
                var cart = _cartRepository.TryGetById(user.Id);
                var cartViewModel = Mapping.ToCartViewModel(cart);
                var productsAmount = cartViewModel?.Amount ?? 0;

                return View("cart", productsAmount);
            }

            return View("cart", 0);
        }


    }
}
