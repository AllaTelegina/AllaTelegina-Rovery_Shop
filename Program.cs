using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models.Repository;
using Backend_asp.net.Models.Service;
using Backend_asp.net.Seeder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//RU: поключения и создание serilog;
Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine("Log", "log.txt"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    .CreateLogger();

//RU: тут подключаем иньекцию к Asp.NetCore;
builder.Host.UseSerilog();

//RU: Строка для того, чтоб можно было еще подгрузить appsettings.Development.json
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

//RU: получение строки подключения для Bd;
var connectionstring = builder.Configuration.GetConnectionString("Defaultconnection");
//RU: регистрирую DbContext в DB;
builder.Services.AddDbContext<AplicationContext>(options=>options.UseSqlServer(connectionstring));

// Automatics create IRepository and ProductRepository. This is DI
builder.Services.AddScoped<MainCategorySeeder>();
builder.Services.AddScoped<IRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).
    AddCookie(opt =>
    {
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);      // время через которое будут удалены cookies с зимней рыбалки;
        opt.Cookie.HttpOnly = true;                         // Kookies доступны только для сервера;
        opt.Cookie.SecurePolicy=CookieSecurePolicy.Always;  // Kookies передаются только по протоколу https;
    }).AddGoogle(optiongoogle =>
    {
        optiongoogle.ClientId = "281783729719-4vnbl39en26sgc79mckpv8dt5fan8tqo.apps.googleusercontent.com";
        optiongoogle.ClientSecret = "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";
        optiongoogle.CallbackPath = "/signin-google";
        optiongoogle.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=consent");
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();   //Добовляем Razor в конвеер сервисов;

// Add services to the container.
// Расширить более директорию для того чоб можно было искать приедставления в других папках;
builder.Services.AddControllersWithViews().AddRazorOptions(option =>
{
    option.ViewLocationFormats.Add("/dist/{0}.cshtml");
});

var app = builder.Build();
app.UseRouting();
app.MapControllers(); // Разрешает использовать API-контроллеры;
app.UseAuthentication(); // Для аутентификации;
app.UseAuthorization();  // Для авторизации;
app.MapRazorPages();    // Подключение Razor;

using(var scope = app.Services.CreateScope())
{
    var seeder =scope.ServiceProvider.GetRequiredService<MainCategorySeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets", "images")),
        RequestPath = "/assets/images"
    });

    app.UseRouting();


    app.MapControllerRoute(
        name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}");
        pattern: "{controller=Product}/{action=ProductsView}/{id?}");   // :DOTO для того, чтоб можно было загрузить нужный контроллер

app.MapAreaControllerRoute(
        name: "Areas",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    app.Run();
