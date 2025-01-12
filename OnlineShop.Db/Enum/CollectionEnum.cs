using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum CollectionEnum
    {
        [Display(Name = "Осень")]
        Autumn,
        [Display(Name = "Зима")]
        Winter,
        [Display(Name = "Весна")]
        Spring,
        [Display(Name = "Лето")]
        Summer
    }
}
