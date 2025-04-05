using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CompareRepository : ICompareRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public CompareRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            this._appDbContext = appDbContext;
            _userManager = userManager;
        }


        public async Task<Compare?> GetById(string userId)
        {
            return await _appDbContext.Compares
                .Include(compare => compare.Products)
                .FirstOrDefaultAsync(productsCompare => productsCompare.User.Id == userId);
        }

        public async Task<Product?> GetProductById(Guid Id)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(product => product.Id == Id);
        }

        public async Task Add(Guid id, string userId)
        {
            var compare = GetById(userId);
            if (compare != null)
            {
                var product = GetProductById(id);
                if (product == null)
                    return;

                if (compare.Products.Contains(product))
                    return;

                if (compare.Products.Count > 1)
                {
                    var lastProduct = compare.Products.Last();
                    compare.Products.Remove(lastProduct);
                    compare.Products.Add(product);
                    await _appDbContext.SaveChangesAsync();
                    return;
                }

                compare.Products.Add(product);
               await _appDbContext.SaveChangesAsync();
            }

            else
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                compare = new Compare { User = user };
                _appDbContext.Compares.Add(compare);
                var product = GetProductById(id);
                if (product != null)
                    compare.Products.Add(product);

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task Remove(Guid id, string userId)
        {
            var productsCompare = GetById(userId);
            if (productsCompare != null)
            {
                var product = GetProductById(id);
                if (product != null)
                    productsCompare.Products.Remove(product);
            }

           await _appDbContext.SaveChangesAsync();
        }
    }
}