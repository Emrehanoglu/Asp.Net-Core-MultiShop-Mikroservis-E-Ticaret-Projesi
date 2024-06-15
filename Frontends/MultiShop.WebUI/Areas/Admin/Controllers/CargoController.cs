using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Cargo")]
public class CargoController : Controller
{
    [Route("CargoList")]
    public IActionResult CargoList()
    {
        return View();
    }
}
