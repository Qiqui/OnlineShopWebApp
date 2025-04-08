using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IFavouritesRepository _favoritesRepository;
        private readonly UserManager<User> _userManager;

        public FavoritesController(IProductsRepository productsRepository, IFavouritesRepository favoritesRepository, UserManager<User> userManager)
        {
            _productsRepository = productsRepository;
            _favoritesRepository = favoritesRepository;
            _userManager = userManager;
        }

        public IActionResult Index(string userName, User user)
        {
            if (user.Name == null)
                user = _userManager.FindByNameAsync(userName).Result;

            var favorites = _favoritesRepository.TryGetById(user.Id);
            if (favorites == null)
                return View(null);

            var favoritesVM = favorites.ToFavouritesViewModel();

            return View(favoritesVM);
        }

        public IActionResult Add(Guid id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var product = _productsRepository.TryGetById(id);
            if (product != null)
                _favoritesRepository.Add(id, user.Id);

            return RedirectToAction(nameof(Index), user);
        }

        public IActionResult Remove(Guid id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var product = _productsRepository.TryGetById(id);
            if (product != null)
                _favoritesRepository.Remove(id, user.Id);

            return RedirectToAction(nameof(Index), user);
        }
    }
}
