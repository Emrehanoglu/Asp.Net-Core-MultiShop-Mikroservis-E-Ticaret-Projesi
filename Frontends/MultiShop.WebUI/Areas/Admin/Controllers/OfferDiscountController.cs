using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/OfferDiscount")]
public class OfferDiscountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOfferDiscountService _offerDiscountService;

    public OfferDiscountController(IHttpClientFactory httpClientFactory, IOfferDiscountService offerDiscountService)
    {
        _httpClientFactory = httpClientFactory;
        _offerDiscountService = offerDiscountService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        ViewBag.v0 = "İndirim Teklif İşlemleri";

        var values = await _offerDiscountService.GetAllOfferDiscountAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateOfferDiscount")]
    public IActionResult CreateOfferDiscount()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        ViewBag.v0 = "İndirim Teklif İşlemleri";

        return View();
    }

    [HttpPost]
    [Route("CreateOfferDiscount")]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }

    [Route("DeleteOfferDiscount/{id}")]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        await _offerDiscountService.DeleteOfferDiscountAsync(id); 
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        ViewBag.v0 = "İndirim Teklif İşlemleri";

        var value = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }
}
