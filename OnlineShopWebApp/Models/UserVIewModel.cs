using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 25 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите свой возраст")]
        [Range(16, 100, ErrorMessage = "Возраст должен быть от 16 до 100 лет")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^(?=.*?8)(?=.*\d).{11}$", ErrorMessage = "Введите 11 цифр номера, начиная с \"8\"")]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Неверно указана почта")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Почта должна содержать от 8 до 25 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Длина пароля 7-15 символов. Пароль должен содержать минимум одну цифру, большие и маленькие латинские буквы")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public string ReturnUrl { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
