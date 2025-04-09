using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class ComparisonRepository : IComparisonRepository
    {
        private readonly AppDbContext _appDbContext;

        public ComparisonRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }


        public async Task<Comparison?> GetByIdAsync(string userId)
        {
            return await _appDbContext.Comparisons
                .Include(compare => compare.Products)
                .FirstOrDefaultAsync(productsCompare => productsCompare.UserId == userId);
        }
        public async Task<Comparison> CreateComparisonAsync(string userId)
        {
            var comparison = new Comparison { UserId = userId };
            await _appDbContext.Comparisons.AddAsync(comparison);

            await _appDbContext.SaveChangesAsync();

            return comparison;
        }

        public async Task UpdateAsync(Comparison comparison)
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}