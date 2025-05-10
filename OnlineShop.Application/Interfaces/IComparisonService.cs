using OnlineShop.Application.DTOs;

namespace OnlineShop.Application.Interfaces
{
    public interface IComparisonService
    {
        Task<ComparisonDTO> GetComparisonDtoAsync(string userName);
        Task<ComparisonDTO> AddProductAsync(Guid productId, string userName);
        Task<ComparisonDTO> RemoveProductAsync(Guid productId, string userName);
        /*Task ClearAsync(string userName);*/
    }
}
