using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShop.Db.Models;
using OnlineShop.Db.Helpers;
using System.Net;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string returnUrl)
        {
            var authyotization = new AuthorizationViewModel { ReturnUrl = returnUrl ?? "/Home"};
            return View(authyotization);
        }

        [HttpPost]
        public IActionResult Authorization(AuthorizationViewModel authorization)
        {
            var IsAuthentificated = _signInManager.PasswordSignInAsync(authorization.Email, authorization.Password, authorization.IsRemembered, false).Result;
            if (!IsAuthentificated.Succeeded)
            {
                ModelState.AddModelError("", "Неверно указана почта или пароль");
                return View(nameof(Index), authorization);
            }
            
            if(!string.IsNullOrEmpty(authorization.ReturnUrl))
                return Redirect(authorization.ReturnUrl);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Registration(string returnUrl)
        {
            var userVM = new UserViewModel { ReturnUrl = returnUrl ?? "/Home" };

            return View(userVM);
        }

        [HttpPost]
        public IActionResult Register(Models.UserViewModel userVM)
        {
            if (userVM.Name?.Trim() == userVM.Password?.Trim())
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");

            if (!ModelState.IsValid)
                return View("Registration", userVM);
            var isExists = _userManager.FindByEmailAsync(userVM.Email).Result;
            if (isExists != null)
            {
                ModelState.AddModelError("", "Пользователь с такой почтой уже существует");
                return View("Registration", userVM);
            }

            var user = userVM.ToUser();
            var result = _userManager.CreateAsync(user, userVM.Password).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, Constants.userRoleName).Wait();
                var isRegistered = _signInManager.PasswordSignInAsync(user.UserName, userVM.Password, true, false).Result;
                return Redirect(userVM.ReturnUrl ?? "/Home");
            }

            ModelState.AddModelError("", "При регистрации произошла ошибка");
            return View("Registration", userVM);
        }
    }
}
