using OnlineShop.Domain.Common;

namespace OnlineShop.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<Guid>> RegisterAsync(string email, string password, string fullName);
        Task<Result> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
