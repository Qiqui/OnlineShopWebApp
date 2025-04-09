using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Enums;
using OnlineShop.Infrastructure.Helpers;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _databaseContext;

        public OrdersRepository(AppDbContext appDbContext)
        {
            _databaseContext = appDbContext;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order?> GetLastByUserIdAsync(string userId)
        {
            return _databaseContext.Orders
                .Where(order => order.UserId == userId)
                .OrderByDescending(order => order.CreateDate)
                .FirstOrDefault();
        }

        public async Task<int> GetCountAsync()
        {
            return await _databaseContext.Orders.CountAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _databaseContext.Orders.AddAsync(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateStatusAsync(Guid id, OrderStatusEnum status)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _databaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}