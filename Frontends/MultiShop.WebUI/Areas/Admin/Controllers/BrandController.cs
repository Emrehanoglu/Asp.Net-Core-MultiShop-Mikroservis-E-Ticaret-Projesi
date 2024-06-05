using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Brand")]
public class BrandController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IBrandService _brandService;

    public BrandController(IHttpClientFactory httpClientFactory, IBrandService brandService)
    {
        _httpClientFactory = httpClientFactory;
        _brandService = brandService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markala";
        ViewBag.v3 = "Marka Listesi";
        ViewBag.v0 = "Marka İşlemleri";

        var values = await _brandService.GetAllBrandAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateBrand")]
    public IActionResult CreateBrand()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markala";
        ViewBag.v3 = "Marka Listesi";
        ViewBag.v0 = "Marka İşlemleri";
        return View();
    }

    [HttpPost]
    [Route("CreateBrand")]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        await _brandService.CreateBrandAsync(createBrandDto);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }

    [Route("DeleteBrand/{id}")]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        await _brandService.DeleteBrandAsync(id);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateBrand/{id}")]
    public async Task<IActionResult> UpdateBrand(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markala";
        ViewBag.v3 = "Marka Listesi";
        ViewBag.v0 = "Marka İşlemleri";

        var value = await _brandService.GetByIdBrandAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateBrand/{id}")]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        await _brandService.UpdateBrandAsync(updateBrandDto);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }
}
