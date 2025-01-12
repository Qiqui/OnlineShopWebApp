using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICompareRepository _productsCompareRepository;
        private readonly UserManager<User> _userManager;

        public CompareController(IProductsRepository productsRepository, ICompareRepository productCompare, UserManager<User> userManager)
        {
            _productsRepository = productsRepository;
            _productsCompareRepository = productCompare;
            _userManager = userManager;
        }

        public IActionResult Index(string userName, User user)
        {
            if (user.Name == null)
                user = _userManager.FindByNameAsync(userName).Result;

            var productsCompare = _productsCompareRepository.TryGetById(user.Id);
            if (productsCompare == null)
            {
                return View(null);
            }

            var productsCompareVM = productsCompare.ToCompareViewModel();

            return View(productsCompareVM);
        }

        public IActionResult Add(Guid id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            _productsCompareRepository.Add(id, user.Id);

            return RedirectToAction(nameof(Index), user);
        }

        public IActionResult Remove(Guid id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            _productsCompareRepository.Remove(id, user.Id);

            return RedirectToAction(nameof(Index), user);
        }
    }
}
