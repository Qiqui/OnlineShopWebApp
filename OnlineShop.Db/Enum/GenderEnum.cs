using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum GenderEnum
    {
        [Display(Name = "Мужской")]
        Man,
        [Display(Name = "Женский")]
        Woman
    }
}
