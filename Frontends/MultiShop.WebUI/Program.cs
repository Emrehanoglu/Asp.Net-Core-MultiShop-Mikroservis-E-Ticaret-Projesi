using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImagesServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //sisteme ilk defa gelen kullan�c�n�n y�nlenece�i sayfa
        opt.LogoutPath = "/Login/Logout/"; //��k�� yap�ld�g�nda yonlenecegi sayfa
        opt.AccessDeniedPath = "/Pages/AccessDenied/"; //kullan�c� yetkisi olmayan bir sayfaya girmeye cal�s�rsa y�nleneceg� sayfa
        opt.Cookie.HttpOnly = true; //https yerine http kullan�m� gecerli olacak

        //Cookie Konfig�rasyonlar�
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "MultiShopJwt";
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //sisteme ilk defa gelen kullan�c�n�n y�nlenece�i sayfa
        opt.ExpireTimeSpan = TimeSpan.FromDays(5); //token s�resi bugunden itibaren 5 g�n olacak
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IClientCredentialTokenService,ClientCredentialTokenService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

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