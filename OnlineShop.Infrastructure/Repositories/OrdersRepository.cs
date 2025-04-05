using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Enums;
using OnlineShop.Db.Helpers;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Persistence;

namespace OnlineShop.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public OrdersRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _databaseContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<Order?> GetById(Guid id)
        {
            return  await _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<List<Order>> GetByUserId(string userId)
        {
            return _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .Where(order => order.User.Id == userId)
                .ToList();
        }

        public async Task<Order?> GetLastByUserId(string userId)
        {
            return _databaseContext.Orders
                .Where(order => order.User.Id == userId)
                .OrderByDescending(order => order.CreateDate)
                .FirstOrDefault();
        }

        public async Task<List<CartPosition>> GetCartPositions(Guid id)
        {
            return await _databaseContext.Carts
                .FirstOrDefaultAsync(cart => cart.Id == id)?
                .Positions;
        }

        public int IncreaseNumber()
        {
            return _databaseContext.Orders.Count() + 1;
        }

        public async Task<bool> Add(Guid cartId, string userId, ContactInfo contactInfo)
        {
            var cartPositions = GetCartPositions(cartId);
            var user = _userManager.FindByIdAsync(userId).Result;
            if (cartPositions != null)
            {
                var order = new Order()
                {
                    User = user,
                    Positions = cartPositions.ToOrderPositions(),
                    ContactInfo = contactInfo,
                    CreateDate = DateTime.Now,
                    Number = IncreaseNumber()
                };

                _databaseContext.Orders.AddAsync(order);
                _databaseContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateStatus(Guid id, OrderStatusEnum status)
        {
            var order = GetById(id);
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