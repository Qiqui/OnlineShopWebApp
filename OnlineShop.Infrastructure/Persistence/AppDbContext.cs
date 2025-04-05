using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Identity;

namespace OnlineShop.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Compare> Compares { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.NewGuid(), Name = "Сумка Pinko", Cost = 44240, Description = @"Сумка выполнена из натуральной кожи. 
Особенности: закрывается на клапан с замок, внутри средник на молнии, 1 отделение на кнопке, 
1 накладной карман, плечевой ремень-цепь с кожаной вставкой под плечо, 
плечевой ремень перетягивается в две ручки.", Category = CategoryEnum.Bag, Color = ColorEnum.Black, Material = MaterialEnum.Leather, Gender = GenderEnum.Woman, Brand = BrandEnum.Pinko},
                new Product { Id = Guid.NewGuid(), Name = "Сумка пляжная Pinko", Cost = 14258, Description = @"Фуксия канвас вышитый логотип спереди 
плетеная отделка необработанные края застежка на молнии сверху две закругленные верхние ручки 
основное отделение внутренняя вставка с логотипом внутренний карман на молнии внутренний накладной 
карман цельная подкладка прямоугольная форма", Category = CategoryEnum.Bag, Color = ColorEnum.Black, Material = MaterialEnum.Leather, Gender = GenderEnum.Woman, Brand = BrandEnum.Pinko},
                new Product { Id = Guid.NewGuid(), Name = "Очки", Cost = 500, Description = "Обычные очки", Category = CategoryEnum.Glasses, Color = ColorEnum.Gray, Material = MaterialEnum.Cotton, Gender = GenderEnum.Woman, Brand = BrandEnum.Gucci}
            );
        }
    }
}
