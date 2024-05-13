using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

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

    [HttpGet]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7260/api/Categories");
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        List<SelectListItem> categoryValues = (from x in values
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues;

        return View();
    }

    [HttpPost]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PostAsync("https://localhost:7260/api/Products", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        return View();
    }
}
