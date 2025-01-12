using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum ColorEnum
    {
        [Display(Name = "Красный")]
        Red,
        [Display(Name = "Зелёный")]
        Green,
        [Display(Name = "Синий")]
        Blue,
        [Display(Name = "Белый")]
        White,
        [Display(Name = "Чёрный")]
        Black,
        [Display(Name = "Жёлтый")]
        Yellow,
        [Display(Name = "Серый")]
        Gray
    }
}
