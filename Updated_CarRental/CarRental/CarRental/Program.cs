using CarRental.Models.AppDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddLazyCache();


builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.ViewLocationFormats.Clear();
    o.ViewLocationFormats.Add("/Data/Home/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Account/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Fuels/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/BodyTypes/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Rentals/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/ReservationStatus/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Transmissions/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/TypeOfDrivings/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Vehicles/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/VehicleTypes/Views/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/Shared/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Data/{0}" + RazorViewEngine.ViewExtension);
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
    
app.Run();

