using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    class CartsService : ICartsService
    {
        private readonly IProductsService _productsService;
        private readonly ICartsRepository _cartsRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;



        public CartsService(ICartsRepository cartRepository, IUsersService userService, IProductsService productsService, IMapper mapper)
        {
            _cartsRepository = cartRepository;
            _usersService = userService;
            _productsService = productsService;
            _mapper = mapper;
        }

        public async Task<Cart> GetByIdAsync(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var cart = await _cartsRepository.GetByIdAsync(userId) ?? await _cartsRepository.CreateCartAsync(userId);

                return cart;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<CartDTO> AddPositionAsync(Guid productId, string userName)
        {
            try
            {
                var product = await _productsService.GetByIdAsync(productId);
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var cart = await GetByIdAsync(userId);

                await IncreasePosition(cart, productId);
                await _cartsRepository.UpdateAsync(cart);

                var cartDTO = GetCartDTO(cart);

                return cartDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task IncreasePosition(Cart cart, Guid productId)
        {
            var product = await _productsService.GetByIdAsync(productId);

            var position = cart.Positions.FirstOrDefault(cartPosition => cartPosition.Product.Id == product.Id);

            if (position != null)
                position.Quantity++;

            else
                cart.Positions.Add(new CartPosition
                {
                    Product = product,
                    Quantity = 1,
                    Cart = cart
                });
        }

        public async Task<CartDTO> RemovePositionAsync(Guid productId, string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var cart = await GetByIdAsync(userId);

                await DecreasePositionAsync(cart, productId);
                await _cartsRepository.UpdateAsync(cart);

                var CartDTO = GetCartDTO(cart);

                return CartDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task DecreasePositionAsync(Cart cart, Guid productId)
        {
            var product = await _productsService.GetByIdAsync(productId);

            var position = cart.Positions.FirstOrDefault(cartPosition => cartPosition.Product.Id == product.Id);
            if (position != null)
            {
                if (position.Quantity > 1)
                    position.Quantity--;
                else
                    cart.Positions.Remove(position);
            }
        }

        public async Task ClearAsync(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var cart = await GetByIdAsync(userId);

                if (cart != null)
                {
                    cart.Positions.Clear();
                    await _cartsRepository.UpdateAsync(cart);
                }
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public CartDTO GetCartDTO(Cart cart)
        {
            var cartDTO = _mapper.Map<CartDTO>(cart);

            return cartDTO;
        }
    }
}
