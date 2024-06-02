using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        //JwtRegisteredClaimNames.Sub ile kullanıcının id 'sine erişim saglayacagım
        var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

        var user = await _userManager.FindByIdAsync(userClaim.Value);
        return Ok(new
        {
            Id = user.Id,
            Name = user.Name, 
            Surname = user.Surname,
            Email = user.Email,
            UserName = user.UserName
        });
    }
}
