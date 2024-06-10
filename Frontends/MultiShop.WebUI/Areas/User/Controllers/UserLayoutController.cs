using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers;

[Area("User")]
[Route("User/UserLayout")]
public class UserLayoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
