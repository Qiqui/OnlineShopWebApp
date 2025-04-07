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
        private readonly UserManager<User> _userManager; //TODO: Убрать, возможно, не понадобится

        public CompareRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            this._appDbContext = appDbContext;
            _userManager = userManager;
        }


        public async Task<Compare?> GetByIdAsync(string userId)
        {
            return await _appDbContext.Compares
                .Include(compare => compare.Products)
                .FirstOrDefaultAsync(productsCompare => productsCompare.UserId == userId);
        }

        public async Task<Product?> GetProductByIdAsync(Guid Id)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(product => product.Id == Id);
        }

        public async Task AddAsync(Guid id, string userId)
        {
            var compare = await GetByIdAsync(userId);
            if (compare != null)
            {
                var product = await GetProductByIdAsync(id);
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
                compare = new Compare { UserId = userId };
                await _appDbContext.Compares.AddAsync(compare);
                var product = await GetProductByIdAsync(id);
                if (product != null)
                    compare.Products.Add(product);

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(Guid id, string userId)
        {
            var productsCompare = await GetByIdAsync(userId);
            if (productsCompare != null)
            {
                var product = await GetProductByIdAsync(id);
                if (product != null)
                    productsCompare.Products.Remove(product);
            }

           await _appDbContext.SaveChangesAsync();
        }
    }
}