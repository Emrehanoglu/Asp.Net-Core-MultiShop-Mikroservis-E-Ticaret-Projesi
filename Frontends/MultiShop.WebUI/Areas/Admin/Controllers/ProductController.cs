using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Product")]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IHttpClientFactory httpClientFactory, 
        IProductService productService, 
        ICategoryService categoryService)
    {
        _httpClientFactory = httpClientFactory;
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
        ViewBag.v0 = "Ürün İşlemleri";

        var values = await _productService.GetAllProductAsync();
        return View(values);
    }

    [HttpGet]
    [Route("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
        ViewBag.v0 = "Ürün İşlemleri";
        //var client = _httpClientFactory.CreateClient();
        //var responseMessage = await client.GetAsync("https://localhost:7260/api/Products/GetProductsWithCategory");
        //if (responseMessage.IsSuccessStatusCode)
        //{
        //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //    var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
        //    return View(values);
        //}
        return View();
    }

    [HttpGet]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
        ViewBag.v0 = "Ürün İşlemleri";

        var values = await _categoryService.GetAllCategoryAsync();
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
        await _productService.CreateProductAsync(createProductDto);
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }

    [Route("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction("Index","Product", new { area = "Admin"});
    }

    [HttpGet]
    [Route("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
        ViewBag.v0 = "Ürün İşlemleri";

        var values = await _categoryService.GetAllCategoryAsync();
        List<SelectListItem> categoryValues = (from x in values
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues;

        var value = await _productService.GetByIdProductAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        await _productService.UpdateProductAsync(updateProductDto);
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }
}