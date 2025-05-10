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
    public class ComparisonController : Controller
    {
        private readonly IComparisonService _comparisonService;
        private readonly IMapper _mapper;

        public ComparisonController(IComparisonService comparesService, IMapper mapper)
        {
            _comparisonService = comparesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string userName)
        {
            try
            {
                var comparisonDTO = await _comparisonService.GetComparisonDtoAsync(userName);
                var comparisonVM = GetComparisonViewModel(comparisonDTO);

                return View(comparisonVM);
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
                var comparisonDTO = await _comparisonService.AddProductAsync(productId, userName);
                var comparisonVM = GetComparisonViewModel(comparisonDTO);

                return View(nameof(Index), comparisonVM);
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
                var comparisonDTO = await _comparisonService.RemoveProductAsync(productId, userName);
                var comparisonVM = GetComparisonViewModel(comparisonDTO);

                return View(nameof(Index), comparisonVM);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public ComparisonViewModel GetComparisonViewModel(ComparisonDTO comparisonDTO)
        {
            var comparisonVM = _mapper.Map<ComparisonViewModel>(comparisonDTO);

            return comparisonVM;
        }
    }
}