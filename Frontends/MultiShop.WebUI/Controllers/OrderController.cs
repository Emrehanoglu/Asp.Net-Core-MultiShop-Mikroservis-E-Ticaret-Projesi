using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;

namespace MultiShop.WebUI.Controllers;

public class OrderController : Controller
{
    private readonly IOrderAddressService _orderAddressService;
    private readonly IUserService _userService;

    public OrderController(IOrderAddressService orderAddressService, IUserService userService)
    {
        _orderAddressService = orderAddressService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
    {
        //kullanıcı id 'sini alalım.
        var values = await _userService.GetUserInfo();
        createOrderAddressDto.UserId = values.Id;
        createOrderAddressDto.Description = "aa";

        //create işlemini gercekletirelim
        await _orderAddressService.CreateOrderAddressAsync(createOrderAddressDto);
        return RedirectToAction("Index", "Payment");
    }
}
