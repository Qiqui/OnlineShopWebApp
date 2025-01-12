using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Enum;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public OrdersRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        public Order TryGetById(Guid id)
        {
            return _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .FirstOrDefault(order => order.Id == id);
        }

        public List<Order> TryGetByUserId(string userId)
        {
            return _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .Where(order => order.User.Id == userId)
                .ToList();
        }

        public Order TryGetLastByUserId(string userId)
        {
            return _databaseContext.Orders
                .Where(order => order.User.Id == userId)
                .OrderByDescending(order => order.CreateDate)
                .FirstOrDefault();
        }

        public List<CartPosition> GetCartPositions(Guid id)
        {
            return _databaseContext.Carts
                .FirstOrDefault(cart => cart.Id == id)?
                .Positions;
        }

        public int IncreaseNumber()
        {
            return _databaseContext.Orders.Count() + 1;
        }

        public bool Add(Guid cartId, string userId, ContactInfo contactInfo)
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

                _databaseContext.Orders.Add(order);
                _databaseContext.SaveChanges();

                return true;
            }

            return false;
        }

        public List<Order> GetAll()
        {
            return _databaseContext.Orders
                .Include(order => order.User)
                .Include(order => order.ContactInfo)
                .Include(order => order.Positions)
                    .ThenInclude(position => position.Product)
                .ToList();
        }

        public bool UpdateStatus(Guid id, OrderStatusEnum status)
        {
            var order = TryGetById(id);
            if (order != null)
            {
                order.Status = status;
                _databaseContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}