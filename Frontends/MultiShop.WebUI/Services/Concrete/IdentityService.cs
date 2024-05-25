using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Services.Concrete;

public class IdentityService : IIdentityService
{
    public Task<bool> SignIn(SignUpDto signUpDto)
    {
        //Identity/Config.cs dosyasında tanımlamış oldugum
        //MultiShopVisitorId, MultiShopManagerId, MultiShopAdminId gibi id değerlerini tutacak.
        
        
        return null;
    }
}
