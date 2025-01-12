using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*[a-z]).{4,15}$", ErrorMessage = "Роль должна начинаться с большой буквы и содержать 4 - 15 символов латинского алфавита")]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
