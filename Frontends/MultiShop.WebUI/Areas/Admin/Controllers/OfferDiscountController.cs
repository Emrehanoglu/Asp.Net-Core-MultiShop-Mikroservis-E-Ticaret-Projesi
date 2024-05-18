using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/OfferDiscount")]
public class OfferDiscountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OfferDiscountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        ViewBag.v0 = "İndirim Teklif İşlemleri";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7260/api/OfferDiscounts");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
            return View(values);
        }
        return View();
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
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createOfferDiscountDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PostAsync("https://localhost:7260/api/OfferDiscounts", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }
        return View();
    }

    [Route("DeleteOfferDiscount/{id}")]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"https://localhost:7260/api/OfferDiscounts/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        ViewBag.v0 = "İndirim Teklif İşlemleri";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:7260/api/OfferDiscounts/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(jsonData);
            return View(value);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7260/api/OfferDiscounts", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }
        return View();
    }
}
