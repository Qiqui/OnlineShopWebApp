using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShopWebApp.Models;


namespace OnlineShopWebApp.Controllers
{
    //[Authorize] TODO: Раскомитить попозже
    public class CartController : Controller
    {
        private readonly ICartsService _cartsService;
        private readonly IMapper _mapper;

        public CartController(ICartsService cartsService, IMapper mapper)
        {
            _cartsService = cartsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var cartDTO = await _cartsService.GetCartDtoAsync(userName);

            var cartVM = GetCartViewModel(cartDTO);

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
