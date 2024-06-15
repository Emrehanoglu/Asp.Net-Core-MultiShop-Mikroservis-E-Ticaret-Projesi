﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/User")]
public class UserController : Controller
{
    private readonly IUserIdentityService _userIdentityService;
    private readonly ICargoCustomerService _cargoCustomerService;

    public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService)
    {
        _userIdentityService = userIdentityService;
        _cargoCustomerService = cargoCustomerService;
    }

    [Route("UserList")]
    public async Task<IActionResult> UserList()
    {
        var values = await _userIdentityService.GetAllUserListtAsync();
        return View(values);
    }

    [Route("UserAddressInfo/{id}")]
    public async Task<IActionResult> UserAddressInfo(string id)
    {
        var values = await _cargoCustomerService.GetByIdCargoCustomerInfoAsync(id);
        return View(values);
    }
}
