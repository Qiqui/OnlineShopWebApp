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
        private readonly UserManager<User> _userManager;

        public FavouritesRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }


        public async Task<Favourites?> GetById(string userId)
        {
            return  await _appDbContext.Favourites
                .Include(favourite => favourite.Products)
                .FirstOrDefaultAsync(favorites => favorites.User.Id == userId);
        }
        public async Task<Product?> GetProductById(Guid Id)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(product => product.Id == Id);
        }

        public async Task Add(Guid id, string userId)
        {
            var favorites = GetById(userId);
            if (favorites != null)
            {
                var product = GetProductById(id);
                if (product != null && !favorites.Products.Contains(product))
                    favorites.Products.Add(product);
            }
            else
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                var newFavorites = new Favourites { User = user };
                _appDbContext.Favourites.Add(newFavorites);
                var product = GetProductById(id);
                if (product != null)
                    newFavorites.Products.Add(product);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task Remove(Guid id, string userId)
        {
            var favorites = GetById(userId);
            if (favorites != null)
            {
                var product = GetProductById(id);
                if (product != null)
                    favorites.Products.Remove(product);
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}