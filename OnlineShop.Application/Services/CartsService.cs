using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class CartsService : ICartsService
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

        private async Task<Cart> GetByNameAsync(string userName)
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
                var cart = await GetByNameAsync(userId);

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

        private async Task IncreasePosition(Cart cart, Guid productId)
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
                var cart = await GetByNameAsync(userId);

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

        private async Task DecreasePositionAsync(Cart cart, Guid productId)
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

        public async Task<List<CartPosition>> GetCartPositionsAndClearAsync(string userId)
        {
            try
            {
                var cart = await _cartsRepository.GetByIdAsync(userId);
                var cartPositions = new List<CartPosition>(cart.Positions);
                cart.Positions.Clear();

                await _cartsRepository.UpdateAsync(cart);

                return cartPositions;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task ClearAsync(string userName)
        {
            try
            {
                var cart = await GetByNameAsync(userName);

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

        public async Task<CartDTO> GetCartDtoAsync(string userName)
        {
            try
            {
                var cart = await GetByNameAsync(userName);
                var cartDTO = _mapper.Map<CartDTO>(cart);

                return cartDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        private CartDTO GetCartDTO(Cart cart)
        {
            var cartDTO = _mapper.Map<CartDTO>(cart);

            return cartDTO;
        }
    }
}
