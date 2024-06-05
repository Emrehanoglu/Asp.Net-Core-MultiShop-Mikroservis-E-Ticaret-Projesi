using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/Feature")]
public class FeatureController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFeatureService _featureService;

    public FeatureController(IHttpClientFactory httpClientFactory, IFeatureService featureService)
    {
        _httpClientFactory = httpClientFactory;
        _featureService = featureService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Önce Çıkan Alanlar";
        ViewBag.v3 = "Önce Çıkan Alan Listesi";
        ViewBag.v0 = "Ana Sayfa Önce Çıkan Alan İşlemleri";

        var values = await _featureService.GetAllFeatureAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateFeature")]
    public IActionResult CreateFeature()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Önce Çıkan Alanlar";
        ViewBag.v3 = "Önce Çıkan Alan Listesi";
        ViewBag.v0 = "Ana Sayfa Önce Çıkan Alan İşlemleri";

        return View();
    }

    [HttpPost]
    [Route("CreateFeature")]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        await _featureService.CreateFeatureAsync(createFeatureDto);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }

    [Route("DeleteFeature/{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        await _featureService.DeleteFeatureAsync(id);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Önce Çıkan Alanlar";
        ViewBag.v3 = "Önce Çıkan Alan Listesi";
        ViewBag.v0 = "Ana Sayfa Önce Çıkan Alan İşlemleri";

        var value = await _featureService.GetByIdFeatureAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        await _featureService.UpdateFeatureAsync(updateFeatureDto);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }
}
