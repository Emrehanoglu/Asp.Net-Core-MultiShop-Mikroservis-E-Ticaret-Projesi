using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/User")]
public class UserController : Controller
{
    private readonly IUserIdentityService _userIdentityService;

    public UserController(IUserIdentityService userIdentityService)
    {
        _userIdentityService = userIdentityService;
    }

    [Route("UserList")]
    public async Task<IActionResult> UserList()
    {
        var values = await _userIdentityService.GetAllUserListtAsync();
        return View(values);
    }
}
