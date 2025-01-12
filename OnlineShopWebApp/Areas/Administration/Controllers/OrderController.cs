using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Enum;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Helpers;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{

    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _orderRepository;

        public OrderController(IOrdersRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult GetAll()
        {
            var orders = _orderRepository.GetAll();
            var ordersVM = orders.ToOrdersViewModel();

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