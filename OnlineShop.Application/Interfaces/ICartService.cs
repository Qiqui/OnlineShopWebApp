using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface ICartService
    {
        /*Task<Cart?> GetById(string userId);
        Task AddPosition(string userId);*/
        Task<Cart> Get(string userId);
        Task AddPosition(Product product, string userId);
        Task RemovePosition(Product product, string userId);
        /*Task<bool> Clear(string userId);*/
    }
}
