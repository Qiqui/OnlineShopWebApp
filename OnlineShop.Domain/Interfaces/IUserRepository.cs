using OnlineShop.Domain.Common;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
    }
}
