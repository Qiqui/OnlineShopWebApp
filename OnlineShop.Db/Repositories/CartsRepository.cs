using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<User> _userManager;

        public CartsRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        public Cart TryGetById(string userId)
        {
            return _databaseContext.Carts
                .Include(cart => cart.Positions)
                .ThenInclude(position => position.Product)
                .FirstOrDefault(cart => cart.User.Id == userId);
        }

        public void Add(User user)
        {
            _databaseContext.Carts.Add(new Cart { User = user });
            _databaseContext.SaveChanges();
        }

        public void Add(Product product, User user)
        {
            var cart = TryGetById(user.Id);
            cart ??= new Cart { User = user };
            if(!_databaseContext.Carts.Contains(cart))
                _databaseContext.Carts.Add(cart);

            var position = cart.Positions.FirstOrDefault(cartPosition => cartPosition.Product.Id == product.Id);

            if (position != null)
                position.Quantity++;

            else
                cart.Positions.Add(new CartPosition
                {
                    Product = product,
                    Quantity = 1,
                    Cart = cart
                });

            _databaseContext.SaveChanges();
        }

        public void Remove(Product product, User user)
        {
            var cart = TryGetById(user.Id);
            var position = cart.Positions.FirstOrDefault(cartPosition => cartPosition.Product.Id == product.Id);
            if (position != null)
            {
                if (position.Quantity > 1)
                    position.Quantity--;
                else
                    cart.Positions.Remove(position);
            }

            _databaseContext.SaveChanges();
        }

        public bool Clear(string userId)
        {
            var cart = TryGetById(userId);
            if (cart != null)
            {
                cart.Positions.Clear();
                _databaseContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
