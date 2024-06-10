using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers;

[Area("User")]
[Route("User/MyOrder")]
public class MyOrderController : Controller
{
    public IActionResult MyOrderList()
    {
        return View();
    }
}
