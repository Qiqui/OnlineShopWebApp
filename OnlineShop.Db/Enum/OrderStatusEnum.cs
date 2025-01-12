using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Enum
{
    public enum OrderStatusEnum
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        InTransist,
        [Display(Name = "Отменён")]
        Canceled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
