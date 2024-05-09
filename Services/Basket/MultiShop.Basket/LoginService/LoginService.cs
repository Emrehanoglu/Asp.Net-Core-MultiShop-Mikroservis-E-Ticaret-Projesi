namespace MultiShop.Basket.LoginService;

public class LoginService : ILoginService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    //token bilgisi içerisindeki sub bilgisinde kullanıcı id bilgisi var onu almış oluyorum.
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
}
