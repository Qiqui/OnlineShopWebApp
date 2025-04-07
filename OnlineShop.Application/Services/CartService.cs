using Microsoft.AspNetCore.Identity;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    class CartService : ICartService
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IUsersService _userService;

        public CartService(ICartsRepository cartRepository, IUsersService userService)
        {
            _cartsRepository = cartRepository;
            _userService = userService;
        }

        public async Task<Cart> GetByIdAsync(string userName)
        {
            var userId = await _userService.GetCurrentUserIdAsync(userName);
            var cart = await _cartsRepository.GetByIdAsync(userId) ?? await _cartsRepository.CreateCartAsync(userId);

            return cart;
        }

        public async Task AddPositionAsync(Product product, string userId)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var cart = await GetByIdAsync(userId);

            await _cartsRepository.AddPositionAsync(cart, product);
        }

        public async Task RemovePositionAsync(Product product, string userId)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var cart = await _cartsRepository.GetByIdAsync(userId);
            if (cart == null)
            {
                throw new NotFoundException($"Ошибка удаления товара из корзины: корзина не найдена");
            }

            await _cartsRepository.RemovePositionAsync(cart, product);
        }
    }
}
