using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _ordersService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService ordersService, IUsersService usersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var orderVM = new OrderViewModel(userId);
                return View(orderVM);
            }

            catch(NotFoundException ex)
            {
                return View(); // TODO: добавить вью ошибки
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Buy(OrderViewModel orderVM)
        {
            if (!orderVM.ContactInfo.IsAgreeWithDataProcessing)
                ModelState.AddModelError("", "Необходимо дать согласие на обработку персональных данных");

            if (!ModelState.IsValid)
                return View("Create", orderVM);

            try
            {
                var orderDTO = GetOrderDTO(orderVM);
                orderDTO = await _ordersService.CreateAsync(orderDTO);
                orderVM = GetOrderViewModel(orderDTO);

                return View(orderVM);
            }

            catch(NotFoundException ex)
            {
                return View(); //TODO: добавить вью ошибки
            }
        }

        public OrderDTO GetOrderDTO(OrderViewModel orderVM)
        {
            var orderDTO = _mapper.Map<OrderDTO>(orderVM);

            return orderDTO;
        }

        public OrderViewModel GetOrderViewModel(OrderDTO orderDTO)
        {
            var orderVM = _mapper.Map<OrderViewModel>(orderDTO);

            return orderVM;
        }
    }
}