using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ImageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4a2fd280-5c11-4e75-9c92-1f0cd6ea3fee"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d7490431-1f31-44b5-86b5-2082f366689f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb73ad23-fba4-4da2-98d4-3cd927b2c55c"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Collection", "Color", "Cost", "Description", "Gender", "ImagePaths", "Material", "Name" },
                values: new object[,]
                {
                    { new Guid("37b26dc6-f116-4d07-bdee-2e6fb2397ed7"), 1, 0, 0, 4, 14258m, "Фуксия канвас вышитый логотип спереди \r\nплетеная отделка необработанные края застежка на молнии сверху две закругленные верхние ручки \r\nосновное отделение внутренняя вставка с логотипом внутренний карман на молнии внутренний накладной \r\nкарман цельная подкладка прямоугольная форма", 1, "[]", 2, "Сумка пляжная Pinko" },
                    { new Guid("9a64189e-e464-413a-9f14-87996f0866d2"), 1, 0, 0, 4, 44240m, "Сумка выполнена из натуральной кожи. \r\nОсобенности: закрывается на клапан с замок, внутри средник на молнии, 1 отделение на кнопке, \r\n1 накладной карман, плечевой ремень-цепь с кожаной вставкой под плечо, \r\nплечевой ремень перетягивается в две ручки.", 1, "[]", 2, "Сумка Pinko" },
                    { new Guid("e76f0f0d-bdfa-4058-ad47-f1a701c85c8a"), 2, 2, 0, 6, 500m, "Обычные очки", 1, "[]", 1, "Очки" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37b26dc6-f116-4d07-bdee-2e6fb2397ed7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9a64189e-e464-413a-9f14-87996f0866d2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e76f0f0d-bdfa-4058-ad47-f1a701c85c8a"));

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Collection", "Color", "Cost", "Description", "Gender", "ImagePaths", "Material", "Name" },
                values: new object[,]
                {
                    { new Guid("4a2fd280-5c11-4e75-9c92-1f0cd6ea3fee"), 1, 0, 0, 4, 14258m, "Фуксия канвас вышитый логотип спереди \r\nплетеная отделка необработанные края застежка на молнии сверху две закругленные верхние ручки \r\nосновное отделение внутренняя вставка с логотипом внутренний карман на молнии внутренний накладной \r\nкарман цельная подкладка прямоугольная форма", 1, "[]", 2, "Сумка пляжная Pinko" },
                    { new Guid("d7490431-1f31-44b5-86b5-2082f366689f"), 2, 2, 0, 6, 500m, "Обычные очки", 1, "[]", 1, "Очки" },
                    { new Guid("eb73ad23-fba4-4da2-98d4-3cd927b2c55c"), 1, 0, 0, 4, 44240m, "Сумка выполнена из натуральной кожи. \r\nОсобенности: закрывается на клапан с замок, внутри средник на молнии, 1 отделение на кнопке, \r\n1 накладной карман, плечевой ремень-цепь с кожаной вставкой под плечо, \r\nплечевой ремень перетягивается в две ручки.", 1, "[]", 2, "Сумка Pinko" }
                });
        }
    }
}
