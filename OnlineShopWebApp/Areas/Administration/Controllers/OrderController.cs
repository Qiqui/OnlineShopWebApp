using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{

    [Area("Administration")]
    [Authorize(Roles = "Admin" /*Constants.AdminRoleName*/)]
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrderController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public async Task<IActionResult> GetAll()
        {
            var orders = await _ordersService.GetAllOrdersDTO();
            var ordersVM = _mapper.Map<List<OrderViewModel>>(orders);

            return View("Orders", ordersVM);
        }

        public IActionResult Order(Guid id)
        {
            var order = _orderRepository.TryGetById(id);
            if (order == null)
                return RedirectToAction(nameof(GetAll));

            var orderVM = order.ToOrderViewModel();

            return View(orderVM);
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid id, OrderStatusEnum status)
        {
            _orderRepository.UpdateStatus(id, status);

            return RedirectToAction(nameof(GetAll));
        }
    }
}