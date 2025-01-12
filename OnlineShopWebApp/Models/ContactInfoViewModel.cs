using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ContactInfoViewModel
    {
        Guid Id { get; set; }
        Guid userId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 25 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Неверно указана почта")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Почта должна содержать от 8 до 25 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [StringLength(80, MinimumLength = 15, ErrorMessage = "Длина адреса должна быть минимум 15 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^(?=.*?8)(?=.*\d).{11}$", ErrorMessage = "Введите 11 цифр номера, начиная с \"8\"")]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        public bool IsAgreeWithDataProcessing { get; set; }
    }
}
