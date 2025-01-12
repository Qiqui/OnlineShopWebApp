using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;

namespace OnlineShop.Db.Repositories
{
    public class CompareRepository : ICompareRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public CompareRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }


        public Compare TryGetById(string userId)
        {
            return _databaseContext.Compares
                .Include(compare => compare.Products)
                .FirstOrDefault(productsCompare => productsCompare.User.Id == userId);
        }

        public Product TryGetProductById(Guid Id)
        {
            return _databaseContext.Products
                .FirstOrDefault(product => product.Id == Id);
        }

        public void Add(Guid id, string userId)
        {
            var compare = TryGetById(userId);
            if (compare != null)
            {
                var product = TryGetProductById(id);
                if (product == null)
                    return;

                if (compare.Products.Contains(product))
                    return;

                if (compare.Products.Count > 1)
                {
                    var lastProduct = compare.Products.Last();
                    compare.Products.Remove(lastProduct);
                    compare.Products.Add(product);
                    _databaseContext.SaveChanges();
                    return;
                }

                compare.Products.Add(product);
                _databaseContext.SaveChanges();
            }

            else
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                compare = new Compare { User = user };
                _databaseContext.Compares.Add(compare);
                var product = TryGetProductById(id);
                if (product != null)
                    compare.Products.Add(product);

                _databaseContext.SaveChanges();
            }
        }

        public void Remove(Guid id, string userId)
        {
            var productsCompare = TryGetById(userId);
            if (productsCompare != null)
            {
                var product = TryGetProductById(id);
                if (product != null)
                    productsCompare.Products.Remove(product);
            }

            _databaseContext.SaveChanges();
        }
    }
}