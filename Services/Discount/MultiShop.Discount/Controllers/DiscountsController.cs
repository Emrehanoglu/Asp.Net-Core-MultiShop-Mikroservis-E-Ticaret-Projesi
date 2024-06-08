using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }
    [HttpGet]
    public async Task<IActionResult> DiscountCouponList()
    {
        var values = await _discountService.GetAllCouponAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountCouponById(int id)
    {
        var values = await _discountService.GetByIdCouponAsync(id);
        return Ok(values);
    }

    [HttpGet("GetDiscountDetailByCode/{code}")]
    public async Task<IActionResult> GetDiscountDetailByCode(string code)
    {
        var values = await _discountService.GetDiscountDetailByCode(code);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
    {
        await _discountService.CreateCouponAsync(createCouponDto);
        return Ok("Kupon başarıyla oluşturuldu");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscountCoupon(int id)
    {
        await _discountService.DeleteCouponAsync(id);
        return Ok("Kupon başarıyla silindi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
    {
        await _discountService.UpdateCouponAsync(updateCouponDto);
        return Ok("İndirim kuponu başarıyla güncellendi");
    }
}