using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class AuthorizationViewModel
    {
        [Required(ErrorMessage = "Не указана почта")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Почта должна содержать от 8 до 25 символов")]
        [EmailAddress(ErrorMessage = "Неверно указана почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Пароль должен содержать большую и маленькие латинские буквы и минимум 1 цифру")]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        public string ReturnUrl { get; set; }
    }
}
