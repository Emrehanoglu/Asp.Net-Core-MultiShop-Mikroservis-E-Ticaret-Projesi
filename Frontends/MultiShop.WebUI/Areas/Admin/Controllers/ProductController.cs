using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/Product")]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
        ViewBag.v0 = "Ürün İşlemleri";
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7260/api/Products");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}
