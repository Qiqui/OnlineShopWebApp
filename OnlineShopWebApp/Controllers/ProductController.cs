using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IActionResult Index(Guid id)
        {
            var product = _productsRepository.TryGetById(id);
            if (product == null)
                return View("ProductNotFoundError", "Товар не найден");

            var productVM = product.ToProductViewModel();

            return View(productVM);
        }

    }
}
