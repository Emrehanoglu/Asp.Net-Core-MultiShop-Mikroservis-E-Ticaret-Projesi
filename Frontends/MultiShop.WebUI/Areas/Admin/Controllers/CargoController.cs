using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Cargo")]
public class CargoController : Controller
{
    private readonly ICargoCompanyService _cargoCompanyService;

    public CargoController(ICargoCompanyService cargoCompanyService)
    {
        _cargoCompanyService = cargoCompanyService;
    }

    [Route("CargoCompanyList")]
    public async Task<IActionResult> CargoCompanyList()
    {
        var values = await _cargoCompanyService.GetAllCargoCompanyAsync();
        return View(values);
    }
}
