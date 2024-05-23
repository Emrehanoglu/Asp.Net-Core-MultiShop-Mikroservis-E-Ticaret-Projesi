using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginsController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    [HttpPost]
    public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
    {
        //asagıdaki ilk false parametresi beni hatırla seceneği gibi passwordu hafızaya almasın demiş oldum,
        //ikinci false parametresinde ise yanlıs giriş sonrası kullanıcıyı kitlemesin demiş oldum.
        
        var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
        
        if (result.Succeeded)
        {
            GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
            model.Username = userLoginDto.Username;
            model.Id = "1"; //şimdilik 1 gönderiyorum
            var token = JwtTokenGenerator.GenerateToken(model);
            return Ok(token);
        }
        else
        {
            return Ok("Kullanıcı adı veya şifre hatalı");
        }
    }
}
