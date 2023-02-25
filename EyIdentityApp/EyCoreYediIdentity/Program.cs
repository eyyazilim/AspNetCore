using EyCoreYediIdentity.CustomDescriber;
using EyCoreYediIdentity.DbContext;
using EyCoreYediIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    //opt.Password.RequireDigit = false;
    //opt.Password.RequiredLength = 4;
    //opt.Password.RequireLowercase = false;
    //opt.Password.RequireUppercase = false;
    //opt.Password.RequireNonAlphanumeric = false;
    //opt.SignIn.RequireConfirmedEmail = false;
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<Context>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "EyCookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(25);
    opt.LoginPath = new PathString("/Home/SignIn");
    opt.LogoutPath = new PathString("/Home/Logout");
    opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddDbContext<Context>(opt =>
{
    opt.UseSqlServer("server=localhost; database=EyIdentityDb; integrated security=true;");
});

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
//    RequestPath = "/node_modules",
//});
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
