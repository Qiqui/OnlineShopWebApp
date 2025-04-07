using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Identity;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
namespace OnlineShopWebApp.Controllers
{
    //[Authorize] TODO: Раскомитить попозже
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartsService;
        private readonly IUsersService _usersService;

        public CartController(IProductService productService, ICartService cartsService, IUsersService usersService)
        {
            _productService = productService;
            _cartsService = cartsService;
            _usersService = usersService;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var cart = await _cartsService.GetByIdAsync(userName);

            var cartVM = cart.ToCartViewModel();

            return View(cartVM);
        }

        public async Task<IActionResult> AddPosition(Guid productId, string userName)
        {
            try
            {

                var product = await _productService.GetByIdAsync(productId);

                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                await _cartsService.AddPositionAsync(product, userId);

                var cart = await _cartsService.GetByIdAsync(userId);
                var cartVM = cart.ToCartViewModel();

                return View("index", cartVM);
            }

            catch(NotFoundException ex)
            {
                return RedirectToAction("Inxex"); //TODO: Сделать View для ошибки NotFound
            }
        }

        public IActionResult RemovePosition(Guid productId, string userName)
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
