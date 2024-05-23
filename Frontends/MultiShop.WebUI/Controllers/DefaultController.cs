using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers;

public class DefaultController : Controller
{
    public IActionResult Index()
    {
        var user = User.Claims;
        return View();
    }
}
