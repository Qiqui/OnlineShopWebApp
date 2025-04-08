using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;


namespace OnlineShopWebApp.Controllers
{
    //[Authorize] TODO: Раскомитить попозже
    public class CartController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ICartsService _cartsService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public CartController(IProductsService productService, ICartsService cartsService, IUsersService usersService, IMapper mapper)
        {
            _productService = productService;
            _cartsService = cartsService;
            _usersService = usersService;
            _mapper = mapper;
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
                var cartDTO = await _cartsService.AddPositionAsync(productId, userName);
                var cartVM = GetCartViewModel(cartDTO);

                return View("index", cartVM);
            }

            catch (NotFoundException ex)
            {
                return RedirectToAction("Inxex"); //TODO: Сделать View для ошибки NotFound
            }
        }

        public async Task<IActionResult> RemovePosition(Guid productId, string userName)
        {
            try
            {
                var cartDTO = await _cartsService.RemovePositionAsync(productId, userName);
                var cartVM = GetCartViewModel(cartDTO);

                return View("index", cartVM);
            }

            catch (NotFoundException ex)
            {
                return RedirectToAction("Inxex"); //TODO: Сделать View для ошибки NotFound
            }
        }

        public async Task<IActionResult> ClearAsync(string userName)
        {
            try
            {
                await _cartsService.ClearAsync(userName);

                return View("index");
            }

            catch (NotFoundException ex)
            {
                return View("CartClearError");
            }

        }

        public CartViewModel GetCartViewModel(CartDTO cartDTO)
        {
            var cartVM = _mapper.Map<CartViewModel>(cartDTO);

            return cartVM;
        }
    }
}
