using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Serilog;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Repositories;
using OnlineShop.Db.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("online_shop");


// добавляем контекст DatabaseContext в качестве сервиса в приложение
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    })
                // устанавливаем тип хранилища - наш контекст
                .AddEntityFrameworkStores<DatabaseContext>();

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

// Add services to the container.
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddTransient<ICartsRepository, CartsRepository>();
builder.Services.AddTransient<ICompareRepository, CompareRepository>();
builder.Services.AddTransient<IFavouritesRepository, FavouritesRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();

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
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, rolesManager);
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
