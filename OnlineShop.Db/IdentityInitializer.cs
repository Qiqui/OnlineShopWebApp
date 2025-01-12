using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@mail.ru";
            var password = "Aaaaaaa1";
            var userEmail = "user@mail.ru";
            var userPassword = "Aaaaaaa1";

            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();

            if (roleManager.FindByNameAsync(Constants.userRoleName).Result == null)
                roleManager.CreateAsync(new IdentityRole(Constants.userRoleName)).Wait();

            if (userManager?.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
            }

            if (userManager?.FindByNameAsync(userEmail).Result == null)
            {
                var user = new User { Email = userEmail, UserName = userEmail };
                var result = userManager.CreateAsync(user, userPassword).Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Constants.userRoleName).Wait();
            }
        }
    }
}
