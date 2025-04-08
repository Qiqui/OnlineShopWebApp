using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class ComparisonController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IComparisonService _comparesService;

        public ComparisonController(IProductsService productsService, IComparisonService comparesService)
        {
            _productsService = productsService;
            _comparesService = comparesService;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var ComparesDTO = _comparesService.GetByNameAsync(userName);

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
