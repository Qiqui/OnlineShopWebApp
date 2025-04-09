using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavouritesService _favouritesService;
        private IMapper _mapper;

        public FavoritesController(IFavouritesService favouritesService, IMapper mapper)
        {
            _favouritesService = favouritesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string userName)
        {
            try
            {
                var favouritesDTO = await _favouritesService.GetFavouritesDtoAsync(userName);
                var favouritesVM = GetFavouritesViewModel(favouritesDTO);

                return View(favouritesVM);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IActionResult> Add(Guid productId, string userName)
        {
            try
            {
                var favouritesDTO = await _favouritesService.AddProductAsync(productId, userName);
                var favouritesVM = GetFavouritesViewModel(favouritesDTO);

                return View(nameof(Index), favouritesVM);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IActionResult> Remove(Guid productId, string userName)
        {
            try
            {
                var favouritesDTO = await _favouritesService.RemoveProductAsync(productId, userName);
                var favouritesVM = GetFavouritesViewModel(favouritesDTO);

                return View(nameof(Index), favouritesVM);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public FavouritesViewModel GetFavouritesViewModel(FavouritesDTO favouritesDTO)
        {
            var favouritesVM = _mapper.Map<FavouritesViewModel>(favouritesDTO);

            return favouritesVM;
        }
    }
}
