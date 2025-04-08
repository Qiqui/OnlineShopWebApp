using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductsService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid productId)
        {
            try
            {
                var productDTO = await _productService.GetProductDtoAsync(productId);
                var productVM = GetProductViewModel(productDTO);

                return View(productVM);
            }

            catch(NotFoundException ex)
            {
                return View("ProductNotFoundError", ex.Message);
            }
        }

        public ProductViewModel GetProductViewModel(ProductDTO productDTO)
        {
            var productVM = _mapper.Map<ProductViewModel>(productDTO);

            return productVM;
        }
    }
}
