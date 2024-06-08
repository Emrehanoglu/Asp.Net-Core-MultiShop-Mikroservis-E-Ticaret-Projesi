using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers;

public class DiscountController : Controller
{
    private readonly IDiscountService _discountService;
    private readonly IBasketService _basketService;

    public DiscountController(IDiscountService discountService, IBasketService basketService)
    {
        _discountService = discountService;
        _basketService = basketService;
    }

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
