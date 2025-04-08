using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IComparisonRepository
    {
        Task<Comparison?> GetByIdAsync(string userId);
        Task<Comparison> CreateComparisonAsync(string userId);
        Task UpdateAsync(Comparison comparison);
    }
}
