using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly ICartsService _cartService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public OrderService(IOrdersRepository orderRepository, ICartsService cartService, IUsersService usersService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
        {
            try
            {
                var order = GetOrderFromOrderDTO(orderDTO);
                order.Number = await GenerateNextOrderNumberAsync();
                var cartPositions = await _cartService.GetCartPositionsAndClearAsync(order.UserId);
                order.Positions = GetOrderPositions(cartPositions);
                await _orderRepository.AddAsync(order);

                orderDTO = GetOrderDTO(order);

                return orderDTO;
            }

            catch(NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<int> GenerateNextOrderNumberAsync()
        {
            return await _orderRepository.GetCountAsync() + 1;
        }

        private Order GetOrderFromOrderDTO(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);

            return order;
        }

        private OrderDTO GetOrderDTO(Order order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            return orderDTO;
        }

        private List<OrderPosition> GetOrderPositions(List<CartPosition> cartPositions)
        {
            var orderPositions = _mapper.Map<List<OrderPosition>>(cartPositions);

            return orderPositions;
        }
    }
}
