using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum MaterialEnum
    {
        [Display(Name = "Искусственная кожа")]
        ArtificialLeather,
        [Display(Name = "Хлопок")]
        Cotton,
        [Display(Name = "Кожа")]
        Leather,
        [Display(Name = "Полиэстер")]
        Polyester,
        [Display(Name = "Текстиль")]
        Textile
    }
}
