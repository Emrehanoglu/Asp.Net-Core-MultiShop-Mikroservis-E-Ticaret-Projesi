using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers;

[Area("User")]
[Route("User/MyOrder")]
public class MyOrderController : Controller
{
    [Route("MyOrderList")]
    public IActionResult MyOrderList()
    {
        return View();
    }
}
