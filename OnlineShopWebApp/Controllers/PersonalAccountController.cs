using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class PersonalAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOrdersRepository _ordersRepository;

        public PersonalAccountController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IOrdersRepository ordersRepository)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        public IActionResult Update(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userVM = user.ToUserUpdateViewModel();
            return View(userVM);
        }

        [HttpPost]
        public IActionResult Update(UserUpdateViewModel userVM)
        {
            if (userVM.UploadedFile != null)
            {
                userVM.ImagePath = ImageSaver.UpdateUserImage(_webHostEnvironment, userVM.UploadedFile);
            }

            ModelState.Remove(nameof(userVM.UploadedFile));
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Update));

            var user = _userManager.FindByIdAsync(userVM.Id).Result;
            user.UpdateByUserViewModel(userVM);
            if (userVM.ImagePath != null)
                user.ImagePath = userVM.ImagePath;

            var isUpdated = _userManager.UpdateAsync(user).Result;
            if (!isUpdated.Succeeded)
            {
                ModelState.AddModelError("", "Произошла ошибка при обновлении данных пользователя");
                return View(userVM);
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Orders(string userId)
        {
            var orders = _ordersRepository.TryGetByUserId(userId);
            var ordersVM = orders.ToOrdersViewModel();
            return View(ordersVM);
        }
    }
}
