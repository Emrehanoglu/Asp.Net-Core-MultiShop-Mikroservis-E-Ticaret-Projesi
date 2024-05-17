using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/Feature")]
public class FeatureController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeatureController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Önce Çıkan Alanlar";
        ViewBag.v3 = "Önce Çıkan Alan Listesi";
        ViewBag.v0 = "Ana Sayfa Önce Çıkan Alan İşlemleri";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7260/api/Features");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            return View(values);
        }
        return View();
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
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PostAsync("https://localhost:7260/api/Features", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        return View();
    }

    [Route("DeleteFeature/{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"https://localhost:7260/api/Features/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Önce Çıkan Alanlar";
        ViewBag.v3 = "Önce Çıkan Alan Listesi";
        ViewBag.v0 = "Ana Sayfa Önce Çıkan Alan İşlemleri";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:7260/api/Features/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
            return View(value);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7260/api/Features", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        return View();
    }
}
