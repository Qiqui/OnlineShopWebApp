using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum CategoryEnum
    {
        [Display(Name = "Сумка")]
        Bag,
        [Display(Name = "Ремень")]
        Belt,
        [Display(Name = "Очки")]
        Glasses
    }
}
