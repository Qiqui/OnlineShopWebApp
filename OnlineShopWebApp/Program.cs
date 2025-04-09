using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Profiles;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Infrastructure.Identity;
using OnlineShop.Infrastructure.Persistence;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Infrastructure.Services;
using OnlineShopWebApp.Profiles;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("online_shop");


// добавляем контекст DatabaseContext в качестве сервиса в приложение
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
})
                // устанавливаем тип хранилища - наш контекст
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.LoginPath = "/Auth/Index";
    options.LogoutPath = "/Auth/Logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(CartProfile).Assembly);

// Add services to the container.
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddScoped<ICartsService, CartsService>();
builder.Services.AddScoped<IComparisonRepository, ComparisonRepository>();
builder.Services.AddScoped<IComparisonService, ComparisonService>();
builder.Services.AddScoped<IFavouritesRepository, FavouritesRepository>();
builder.Services.AddScoped<IFavouritesService, FavouritesService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrderService>();
builder.Services.AddScoped<IUsersService, UserService>();

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Shop"));

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    // IdentityInitializer.Initialize(userManager, rolesManager); TODO: РЕШИТЬ ПОЗЖЕ, НУЖЕН ИЛИ НЕТ.
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
