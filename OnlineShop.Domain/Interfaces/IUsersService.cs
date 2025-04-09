namespace OnlineShop.Domain.Interfaces
{
    public interface IUsersService
    {
        Task<string?> GetCurrentUserIdAsync(string userName);
    }
}
