using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IOrdersRepository _orderRepository;
        private readonly UserManager<User> _userManager;

        public OrderController(ICartsRepository cartRepository, IOrdersRepository orderRepository, UserManager<User> userManager)
        {
            _cartsRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public IActionResult Create(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var order = new OrderViewModel(user.Id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Buy(OrderViewModel orderVM)
        {
            if (!orderVM.ContactInfo.IsAgreeWithDataProcessing)
                ModelState.AddModelError("", "Необходимо дать согласие на обработку персональных данных");

            if (!ModelState.IsValid)
                return View("Create", orderVM);

            var cart = _cartsRepository.TryGetById(orderVM.UserId);
            if (cart != null)
            {
                var contactInfo = orderVM.ContactInfo.ToContactInfo();
                _orderRepository.Add(cart.Id, orderVM.UserId, contactInfo);
                orderVM.Positions = cart.Positions.ToCartPositionsViewModel();
            }

            _cartsRepository.Clear(orderVM.UserId);
            var order = _orderRepository.TryGetLastByUserId(orderVM.UserId);
            orderVM.Number = order.Number;

            return View(orderVM);
        }
    }
}