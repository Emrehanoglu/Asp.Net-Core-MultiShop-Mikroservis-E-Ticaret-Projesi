using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/FeatureSlider")]
public class FeatureSliderController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFeatureSliderService _featureSliderService;

    public FeatureSliderController(IHttpClientFactory httpClientFactory, IFeatureSliderService featureSliderService)
    {
        _httpClientFactory = httpClientFactory;
        _featureSliderService = featureSliderService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
        ViewBag.v0 = "Öne Çıkar Slider Görsel İşlemleri";

        var values = await _featureSliderService.GetAllFeatureSliderAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateFeatureSlider")]
    public IActionResult CreateFeatureSlider()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
        ViewBag.v0 = "Öne Çıkar Slider Görsel İşlemleri";
        return View();
    }

    [HttpPost]
    [Route("CreateFeatureSlider")]
    public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
    {
        await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
        return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
    }

    [Route("DeleteFeatureSlider/{id}")]
    public async Task<IActionResult> DeleteFeatureSlider(string id)
    {
        await _featureSliderService.DeleteFeatureSliderAsync(id);
        return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateFeatureSlider/{id}")]
    public async Task<IActionResult> UpdateFeatureSlider(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
        ViewBag.v0 = "Öne Çıkar Slider Görsel İşlemleri";

        var value = await _featureSliderService.GetByIdFeatureSliderAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateFeatureSlider/{id}")]
    public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
        return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
    }
}
