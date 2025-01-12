using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ProductPropsRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2383ed90-d7d5-4bb1-965f-3c2b731a92e6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a449faa4-8968-4865-9e47-fb43ef221172"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bb024be7-a642-4f88-903c-e950b6c108d2"));

            migrationBuilder.RenameColumn(
                name: "ImagesPathes",
                table: "Products",
                newName: "ImagePaths");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ImagePaths",
                table: "Products",
                newName: "ImagesPathes");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Collection", "Color", "Cost", "Description", "Gender", "ImagesPathes", "Material", "Name" },
                values: new object[,]
                {
                    { new Guid("2383ed90-d7d5-4bb1-965f-3c2b731a92e6"), 2, 2, 0, 6, 500m, "Обычные очки", 1, "[]", 1, "Очки" },
                    { new Guid("a449faa4-8968-4865-9e47-fb43ef221172"), 1, 0, 0, 4, 44240m, "Сумка выполнена из натуральной кожи. \r\nОсобенности: закрывается на клапан с замок, внутри средник на молнии, 1 отделение на кнопке, \r\n1 накладной карман, плечевой ремень-цепь с кожаной вставкой под плечо, \r\nплечевой ремень перетягивается в две ручки.", 1, "[]", 2, "Сумка Pinko" },
                    { new Guid("bb024be7-a642-4f88-903c-e950b6c108d2"), 1, 0, 0, 4, 14258m, "Фуксия канвас вышитый логотип спереди \r\nплетеная отделка необработанные края застежка на молнии сверху две закругленные верхние ручки \r\nосновное отделение внутренняя вставка с логотипом внутренний карман на молнии внутренний накладной \r\nкарман цельная подкладка прямоугольная форма", 1, "[]", 2, "Сумка пляжная Pinko" }
                });
        }
    }
}
