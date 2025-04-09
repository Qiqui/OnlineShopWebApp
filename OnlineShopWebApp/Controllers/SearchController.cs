using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductsService _productsService;

        public SearchController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /*[HttpPost]
        public IActionResult Searching(string keyWords)
        {
            var products = _productsService.Search(keyWords);
            var productsVM = products.ToProductsViewModel();

            return View("Index", productsVM);
        }*/
    }
}
