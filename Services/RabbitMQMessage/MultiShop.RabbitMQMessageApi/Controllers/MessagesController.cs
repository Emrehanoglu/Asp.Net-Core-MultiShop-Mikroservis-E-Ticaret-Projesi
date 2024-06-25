using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.RabbitMQMessageApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return Ok("Mesajınız Kuyruga Alınmıstır.");
        }
    }
}
