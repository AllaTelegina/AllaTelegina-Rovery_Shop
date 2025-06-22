using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Расширить более директорию для того чоб можно было искать приедставления в других папках;
builder.Services.AddControllersWithViews().AddRazorOptions(option =>
{
    option.ViewLocationFormats.Clear();
    option.ViewLocationFormats.Add("/dist/{0}.cshtml");
});

var app = builder.Build();
app.MapControllers(); // Разрешает использовать API-контроллеры;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets","images")),
    RequestPath = "/assets/images"
}) ;

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
