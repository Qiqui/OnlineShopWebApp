using Microsoft.AspNetCore.Identity;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    class CartService : ICartService
    {
        private readonly ICartsRepository _cartRepository;
        private readonly IUserService _userService;

        public CartService(ICartsRepository cartRepository, IUserService userService)
        {
            _cartRepository = cartRepository;
            _userService = userService;
        }

        public async Task<Cart> Get(string userName)
        {
            var userId = await _userService.GetCurrentUserIdAsync(userName);
            var cart = await _cartRepository.GetByIdAsync(userId) ?? await _cartRepository.CreateCartAsync(userId);

            return cart;
        }

        public async Task AddPosition(Product product, string userId)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var cart = await Get(userId);

            await _cartRepository.AddPositionAsync(cart, product);
        }

        public async Task RemovePosition(Product product, string userId)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var cart = await _cartRepository.GetByIdAsync(userId);
            if (cart == null)
            {
                throw new NotFoundException($"Ошибка удаления товара из корзины: корзина не найдена");
            }

            await _cartRepository.RemovePositionAsync(cart, product);
        }
    }
}
