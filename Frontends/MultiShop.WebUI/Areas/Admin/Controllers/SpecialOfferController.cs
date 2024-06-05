using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/SpecialOffer")]
public class SpecialOfferController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISpecialOfferService _specialOfferService;

    public SpecialOfferController(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService)
    {
        _httpClientFactory = httpClientFactory;
        _specialOfferService = specialOfferService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";

        var values = await _specialOfferService.GetAllSpecialOfferAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateSpecialOffer")]
    public IActionResult CreateSpecialOffer()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";
        return View();
    }

    [HttpPost]
    [Route("CreateSpecialOffer")]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }

    [Route("DeleteSpecialOffer/{id}")]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        await _specialOfferService.DeleteSpecialOfferAsync(id);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";

        var value = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }
}
