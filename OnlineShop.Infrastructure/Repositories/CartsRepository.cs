using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public CartsRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<Cart?> GetById(string userId)
        {
            return await _appDbContext.Carts
                .Include(cart => cart.Positions)
                .ThenInclude(position => position.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task Add(string userId)
        {
            await _appDbContext.Carts.AddAsync(new Cart { UserId = userId });
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Add(Product product, string userId)
        {
            var cart = await GetById(userId);
            cart ??= new Cart { UserId = userId };
            if(!await _appDbContext.Carts.ContainsAsync(cart))
                await _appDbContext.Carts.AddAsync(cart);

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

            await _appDbContext.SaveChangesAsync();
        }

        public async Task Remove(Product product, string userId)
        {
            var cart = await GetById(userId);
            var position = cart.Positions.FirstOrDefault(cartPosition => cartPosition.Product.Id == product.Id);
            if (position != null)
            {
                if (position.Quantity > 1)
                    position.Quantity--;
                else
                    cart.Positions.Remove(position);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> Clear(string userId)
        {
            var cart = await GetById(userId);
            if (cart != null)
            {
                cart.Positions.Clear();
                await _appDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
