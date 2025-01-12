using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Helpers;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesVM = roles.ToRolesViewModel();
            return View("Roles", rolesVM);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleAddRViewModel role)
        {
            if (_roleManager.FindByNameAsync(role.Name).Result != null)
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View();

            var isAdded = _roleManager.CreateAsync(new IdentityRole(role.Name)).Result;
            if (!isAdded.Succeeded)
            {
                ModelState.AddModelError("", "Роль не была добавлена");
                return View();
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Remove(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
            {
                ModelState.AddModelError("", "Роль не найдена");
                var roles = _roleManager.Roles.ToList();
                return View("Roles", roles);
            }

            var isRemoved = _roleManager.DeleteAsync(role).Result;
            if (!isRemoved.Succeeded)
            {
                ModelState.AddModelError("", "При удалении произошла ошибка");
                var roles = _roleManager.Roles.ToList();
                return View("Roles", roles);
            }

            return RedirectToAction(nameof(GetAll));
        }
    }
}
