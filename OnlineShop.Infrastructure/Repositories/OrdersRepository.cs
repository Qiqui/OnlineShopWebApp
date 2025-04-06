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
        private readonly UserManager<User> _userManager; // TODO: Убрать, возможно, не понадобится

        public OrdersRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _databaseContext = appDbContext;
            _userManager = userManager; // TODO: Убрать, возможно не понадобится
        }

        public async Task<Order?> GetById(Guid id)
        {
            return  await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<List<Order>> GetByUserId(string userId)
        {
            return await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order?> GetLastByUserId(string userId)
        {
            return _databaseContext.Orders
                .Where(order => order.UserId == userId)
                .OrderByDescending(order => order.CreateDate)
                .FirstOrDefault();
        }

        public async Task<List<CartPosition>> GetCartPositions(Guid id)
        {
            var cart = await _databaseContext.Carts
                .Include(cart => cart.Positions)
                .FirstOrDefaultAsync(cart => cart.Id == id);

            return cart?.Positions ?? new List<CartPosition>();
        }

        public async Task<int> IncreaseNumber()
        {
            return await _databaseContext.Orders.CountAsync() + 1;
        }

        public async Task<bool> Add(Guid cartId, string userId, ContactInfo contactInfo)
        {
            var cartPositions = await GetCartPositions(cartId);
            //var user = await _userManager.FindByIdAsync(userId); TODO: УДАЛИТЬ, СКОРЕЕ ВСЕГО НЕ ПОНАДОБИТСЯ
            if (cartPositions != null)
            {
                var order = new Order()
                {
                    UserId = userId,
                    Positions = cartPositions.ToOrderPositions(),
                    ContactInfo = contactInfo,
                    CreateDate = DateTime.Now,
                    Number = await IncreaseNumber()
                };

               await _databaseContext.Orders.AddAsync(order);
               await _databaseContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _databaseContext.Orders
                .Include(order => order.UserId)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateStatus(Guid id, OrderStatusEnum status)
        {
            var order = await GetById(id);
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