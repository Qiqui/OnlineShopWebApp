using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Common;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Result> CreateAsync(User user)
        {
            await _appDbContext.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }
    }
}
