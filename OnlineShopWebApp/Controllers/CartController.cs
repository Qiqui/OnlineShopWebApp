using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces;
using OnlineShop.Infrastructure.Identity;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
namespace OnlineShopWebApp.Controllers
{
    //[Authorize] TODO: Раскомитить попозже
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;

        public CartController(IProductService productService, ICartService cartsService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _cartService = cartsService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var cart = await _cartService.Get(userName);

            var cartVM = cart.ToCartViewModel();

            return View(cartVM);
        }

        /*public IActionResult AddPosition(Guid id, string userName)
        {
            var product = _productsRepository.TryGetById(id);

            if (product == null)
                return View("Add", "Ошибка. Товар не был добавлен");

            var user = _userManager.FindByNameAsync(userName).Result;
            _cartsRepository.Add(product, user);

            var cart = _cartsRepository.TryGetById(user.Id);
            var cartVM = cart.ToCartViewModel();

            return View("index", cartVM);
        }

        public IActionResult RemovePosition(Guid id, string userName)
        {
            var product = _productsRepository.TryGetById(id);

            if (product == null)
                return View("Ошибка. Товар не был удален");

            var user = _userManager.FindByNameAsync(userName).Result;
            _cartsRepository.Remove(product, user);
            var cart = _cartsRepository.TryGetById(user.Id);
            var cartVM = cart.ToCartViewModel();

            return View("index", cartVM);
        }

        public IActionResult TryClear(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var isCartCleared = _cartsRepository.Clear(user.Id);

            if (!isCartCleared)
            {
                return View("CartClearError");
            }

            return View("index");
        }*/
    }
}
