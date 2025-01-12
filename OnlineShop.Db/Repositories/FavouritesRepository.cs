using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public FavouritesRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }


        public Favourites TryGetById(string userId)
        {
            return _databaseContext.Favourites
                .Include(favourite => favourite.Products)
                .FirstOrDefault(favorites => favorites.User.Id == userId);
        }
        public Product TryGetProductById(Guid Id)
        {
            return _databaseContext.Products
                .FirstOrDefault(product => product.Id == Id);
        }

        public void Add(Guid id, string userId)
        {
            var favorites = TryGetById(userId);
            if (favorites != null)
            {
                var product = TryGetProductById(id);
                if (product != null && !favorites.Products.Contains(product))
                    favorites.Products.Add(product);
            }
            else
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                var newFavorites = new Favourites { User = user };
                _databaseContext.Favourites.Add(newFavorites);
                var product = TryGetProductById(id);
                if (product != null)
                    newFavorites.Products.Add(product);
            }

            _databaseContext.SaveChanges();
        }

        public void Remove(Guid id, string userId)
        {
            var favorites = TryGetById(userId);
            if (favorites != null)
            {
                var product = TryGetProductById(id);
                if (product != null)
                    favorites.Products.Remove(product);
            }

            _databaseContext.SaveChanges();
        }
    }
}