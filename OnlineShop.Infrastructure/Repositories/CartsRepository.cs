using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly AppDbContext _appDbContext;

        public CartsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Cart?> GetByIdAsync(string userId)
        {
            return await _appDbContext.Carts
                .Include(cart => cart.Positions)
                .ThenInclude(position => position.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task<Cart> CreateCartAsync(string userId)
        {
            var cart = new Cart { UserId = userId };
            await _appDbContext.Carts.AddAsync(cart);
            await _appDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task UpdateAsync(Cart cart)
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
