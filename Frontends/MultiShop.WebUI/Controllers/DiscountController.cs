using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers;

public class DiscountController : Controller
{
    [HttpGet]
    public PartialViewResult ConfirmDiscountCoupon()
    {
        return PartialView();
    }
    [HttpPost]
    public PartialViewResult ConfirmDiscountCoupon(string code)
    {
        return PartialView();
    }
}
