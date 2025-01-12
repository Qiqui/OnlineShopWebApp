using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public SearchController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpPost]
        public IActionResult Searching(string keyWords)
        {
            var products = _productsRepository.Search(keyWords);
            var productsVM = products.ToProductsViewModel();
            
            return View("Index", productsVM);
        }
    }
}
