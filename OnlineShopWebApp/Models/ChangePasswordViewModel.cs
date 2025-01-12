using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Длина пароля 7-15 символов. Пароль должен содержать минимум одну цифру, большие и маленькие латинские буквы")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public ChangePasswordViewModel() { }
        public ChangePasswordViewModel(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
