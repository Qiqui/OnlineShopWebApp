using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Helpers;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult GetAll()
        {
            var users = _userManager.Users.ToList();
            var usersVM = users.ToUsersViewModel();
            return View("Index", usersVM);
        }

        public IActionResult User(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var userVM = user.ToUserViewModel();
                return View(userVM);
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Update(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var userVM = user.ToUserViewModel();
                return View(userVM);
            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View();
        }

        [HttpPost]
        public IActionResult Update(string id, UserUpdateViewModel userUpdate)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (!ModelState.IsValid)
                return View(nameof(Update));
            user.UpdateByUserViewModel(userUpdate);
            var isUpdated = _userManager.UpdateAsync(user).Result;
            if (!isUpdated.Succeeded)
            {
                ModelState.AddModelError("", "Произошла ошибка при обновлении данных пользователя");
                var userVM = user.ToUserViewModel();
                return View(userVM);
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult ChangePassword(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user != null)
            {
                var changePassword = new ChangePasswordViewModel(id, user.Email);
                return View(changePassword);
            }
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (changePassword.Password != changePassword.PasswordConfirm)
            {
                ModelState.AddModelError("", "Пароли должны совпадать");
                return View();
            }
            var user = _userManager.FindByIdAsync(changePassword.Id).Result;
            var isChanged = _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.Password).Result;
            if (!isChanged.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка смены пароля");
                return View(changePassword);
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult ChangeRole(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var roleName = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            var role = _roleManager.FindByNameAsync(roleName).Result;
            var roleId = _roleManager.GetRoleIdAsync(role).Result;
            var userVM = user.ToUserViewModel();
            var roles = _roleManager.Roles.ToList();
            var rolesVM = roles.ToRolesViewModel();
            var changeRole = new ChangeRoleViewModel(userVM, rolesVM);
            changeRole.RoleId = roleId;
            changeRole.RoleName = roleName;

            return View(changeRole);
        }

        [HttpPost]
        public IActionResult ChangeRole(string userId, ChangeRoleViewModel Role)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var newRole = _roleManager.FindByIdAsync(Role.RoleId).Result;
            if (newRole != null && user != null)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                _userManager.RemoveFromRolesAsync(user, userRoles).Wait();
                var isChanged = _userManager.AddToRoleAsync(user, newRole.Name).Result;

                if (!isChanged.Succeeded)
                {
                    ModelState.AddModelError("", "Не удалось сменить роль");
                    var roles = _roleManager.Roles.ToList();
                    var rolesVM = roles.ToRolesViewModel();
                    var userVM = user.ToUserViewModel();
                    var changeRole = new ChangeRoleViewModel(userVM, rolesVM);
                    return View(changeRole);
                }
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserViewModel userVM)
        {
            var isEmailExists = _userManager.FindByEmailAsync(userVM.Email).Result;
            if (isEmailExists != null)
                ModelState.AddModelError("", "Пользователь существует");

            if (!ModelState.IsValid)
                return View();

            var user = userVM.ToUser();
            var isAdded = _userManager.CreateAsync(user).Result;
            if (!isAdded.Succeeded)
            {
                ModelState.AddModelError("", "Произошла ошибка при добавлении");
                return View(userVM);
            }

            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Remove(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var isRemoved = _userManager.DeleteAsync(user).Result;
                if (!isRemoved.Succeeded)
                {
                    ModelState.AddModelError("", "Произошла ошибка при удалении");
                    var users = _userManager.Users.ToList();
                    var usersVM = users.ToUsersViewModel();
                    return View(usersVM);
                }
            }

            return RedirectToAction(nameof(GetAll));
        }
    }
}
