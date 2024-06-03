using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //sisteme ilk defa gelen kullanýcýnýn yönleneceði sayfa
        opt.LogoutPath = "/Login/Logout/"; //çýkýþ yapýldýgýnda yonlenecegi sayfa
        opt.AccessDeniedPath = "/Pages/AccessDenied/"; //kullanýcý yetkisi olmayan bir sayfaya girmeye calýsýrsa yönlenecegý sayfa
        opt.Cookie.HttpOnly = true; //https yerine http kullanýmý gecerli olacak

        //Cookie Konfigürasyonlarý
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "MultiShopJwt";
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //sisteme ilk defa gelen kullanýcýnýn yönleneceði sayfa
        opt.ExpireTimeSpan = TimeSpan.FromDays(5); //token süresi bugunden itibaren 5 gün olacak
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();