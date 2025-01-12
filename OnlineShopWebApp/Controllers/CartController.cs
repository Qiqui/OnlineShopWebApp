using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;
        private readonly UserManager<User> _userManager;

        public CartController(IProductsRepository productsRepository, ICartsRepository cartRepository, UserManager<User> userManager)
        {
            _productsRepository = productsRepository;
            _cartsRepository = cartRepository;
            _userManager = userManager;
        }

        public IActionResult Index(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var cart = _cartsRepository.TryGetById(user.Id);
            if (cart == null)
            {
                _cartsRepository.Add(user);
                cart = _cartsRepository.TryGetById(user.Id);
            }

            var cartVM = cart.ToCartViewModel();

            return View(cartVM);
        }

        public IActionResult Add(Guid id, string userName)
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

        public IActionResult Remove(Guid id, string userName)
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
        }
    }
}
