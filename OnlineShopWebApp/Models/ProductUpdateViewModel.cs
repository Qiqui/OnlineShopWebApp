using OnlineShop.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductUpdateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(45, MinimumLength = 4, ErrorMessage = "Наименование должно содержать от 4 до 45 символов")]
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(500, 500000, ErrorMessage = "Значение от 500 до 500000")]
        [RegularExpression(@"^(?=.*\d).{3,6}$", ErrorMessage = "Стоимость от 500 до 500000")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(700, MinimumLength = 30, ErrorMessage = "Описание не менее 30, не более 700 символов")]
        public string Description { get; set; } 
        public ColorEnum Color { get; set; }
        public CollectionEnum Collection { get; set; }
        public MaterialEnum Material { get; set; }
        public BrandEnum Brand { get; set; }
        public CategoryEnum Category { get; set; }
        public List<IFormFile>? UploadedFiles { get; set; }
    }
}
