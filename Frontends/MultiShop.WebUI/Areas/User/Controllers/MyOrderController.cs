﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;

namespace MultiShop.WebUI.Areas.User.Controllers;

[Area("User")]
[Route("User/MyOrder")]
public class MyOrderController : Controller
{
    private readonly IOrderOrderingService _orderOrderingService;
    private readonly IUserService _userService;

    public MyOrderController(IOrderOrderingService orderOrderingService, IUserService userService)
    {
        _orderOrderingService = orderOrderingService;
        _userService = userService;
    }

    [Route("MyOrderList")]
    public async Task<IActionResult> MyOrderList()
    {
        var user = await _userService.GetUserInfo();
        var values = _orderOrderingService.GetOrderingByUserId(user.Id);
        return View(values);
    }
}
