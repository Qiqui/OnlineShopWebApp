using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager; //TODO: Убрать, возможно, не понадобится

        public FavouritesRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }


        public async Task<Favourites?> GetByIdAsync(string userId)
        {
            return  await _appDbContext.Favourites
                .Include(favourite => favourite.Products)
                .FirstOrDefaultAsync(favorites => favorites.UserId == userId);
        }
        public async Task<Product?> GetProductByIdAsync(Guid Id)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(product => product.Id == Id);
        }

        public async Task AddAsync(Guid id, string userId)
        {
            var favorites = await GetByIdAsync(userId);
            if (favorites != null)
            {
                var product = await GetProductByIdAsync(id);
                if (product != null && !favorites.Products.Contains(product))
                    favorites.Products.Add(product);
            }
            else
            {
                var newFavorites = new Favourites { UserId = userId };
                await _appDbContext.Favourites.AddAsync(newFavorites);
                var product = await GetProductByIdAsync(id);
                if (product != null)
                    newFavorites.Products.Add(product);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id, string userId)
        {
            var favorites = await GetByIdAsync(userId);
            if (favorites != null)
            {
                var product = await GetProductByIdAsync(id);
                if (product != null)
                    favorites.Products.Remove(product);
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}