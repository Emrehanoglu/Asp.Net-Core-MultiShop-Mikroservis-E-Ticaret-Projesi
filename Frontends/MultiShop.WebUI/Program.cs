using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //sisteme ilk defa gelen kullanýcýnýn yönleneceði sayfa
        opt.LogoutPath = "/Login/Logout/"; //çýkýþ yapýldýgýnda yonlenecegi sayfa
        opt.AccessDeniedPath = "/Pages/AccessDenied/"; //kullanýcý yetkisi olmayan bir sayfaya girmeye calýsýrsa yönlenecegý sayfa
        opt.Cookie.HttpOnly = true; //https yerine http kullanýmý gecerli olacak

        //Cooki Konfigürasyonlarý
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "MultiShopJwt";
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

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