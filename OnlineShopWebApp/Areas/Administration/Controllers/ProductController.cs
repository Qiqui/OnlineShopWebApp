using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{

    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductsRepository productsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productsRepository = productsRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetAll()
        {
            var products = _productsRepository.GetAll();
            var productsVM = products.ToProductsViewModel();
            return View("Products", productsVM);
        }

        public IActionResult Update(Guid id)
        {
            var product = _productsRepository.TryGetById(id);
            if (product == null)
                return RedirectToAction(nameof(GetAll));

            var productUpdateVM = product.ToProductUpdateViewModel();

            return View(productUpdateVM);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel productVM)
        {
            var imagesPathes = new List<string>();
            if (productVM.UploadedFiles != null)
            {
                imagesPathes = _productsRepository.TryGetById(productVM.Id)?.ImagePaths;
                imagesPathes = ImageSaver.UpdateProductImage(_webHostEnvironment, productVM, imagesPathes);
            }

            ModelState.Remove(nameof(productVM.UploadedFiles));
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Update));

            var product = productVM.ToProduct();
            if (imagesPathes != null)
                product.ImagePaths = imagesPathes;

            _productsRepository.Update(productVM.Id, product);

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel productVM)
        {
            if (string.IsNullOrEmpty(productVM?.Name))
                ModelState.AddModelError("", "Необходимо указать название");
            if (productVM.Cost <= 0)
                ModelState.AddModelError("", "Необходимо указать стоимость");
            if (string.IsNullOrEmpty(productVM.Description))
                ModelState.AddModelError("", "Необходимо заполнить описание");
            if (productVM.UploadedFiles == null)
            {
                ModelState.AddModelError("", "Необходимо добавить изображение");
                ModelState.Remove(nameof(productVM.UploadedFiles));
            }
            if (!ModelState.IsValid)
                return View(nameof(Add));

            var imagePaths = ImageSaver.SaveProductImage(_webHostEnvironment, productVM);
            var product = productVM.ToProduct(imagePaths);
            _productsRepository.Add(product);

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Remove(Guid id)
        {
            var isRemoved = _productsRepository.TryToRemove(id);

            return RedirectToAction(nameof(GetAll));
        }
    }
}
