using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MultiShop.WebUI.Controllers;

public class LoginController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Index()
	{
		return View();
	}
    [HttpPost]
    public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
    {
        var client = _httpClientFactory.CreateClient();
        var content = new StringContent(JsonSerializer.Serialize(createLoginDto),Encoding.UTF8,"application/json");
        var responseMessage = await client.PostAsync("http://localhost:5001/api/Logins",content);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();

            //kullanıcı bu noktaya kadar kullanıcı adı ve şifresini girerek login işlemini yaptı,
            //sonrasında bir token üretildi, şimdi bu token bilgisi Deserialize edilerek kontrol edilecek

            var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            //bu noktada Microsoft.AspNetCore.Authentication.JwtBearer paketini WebUI 'ya dahil ettim.

            if (tokenModel != null)
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel.Token);
                var claims = token.Claims.ToList();

                if(tokenModel.Token != null)
                {
                    claims.Add(new Claim("multishoptoken", tokenModel.Token));
                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties
                    {
                        ExpiresUtc=tokenModel.ExpireDate, //token 'ın süresi modelden gelecek olan ExpireDate kadar olsun
                        IsPersistent = true //tarayıcı kapatilsa bile token hatırlansın
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                        authProps);

                    return RedirectToAction("Index","Default");
                }
            }
        }
        return View();
    }
}
