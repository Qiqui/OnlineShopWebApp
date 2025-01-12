using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserUpdateViewModel
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

        [Required(ErrorMessage = "Введите электронную почту")]
        [EmailAddress(ErrorMessage = "Неверно указана электронная почта")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Почта должна содержать от 8 до 25 символов")]
        public string Email { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? UploadedFile { get; set; }
    }
}
