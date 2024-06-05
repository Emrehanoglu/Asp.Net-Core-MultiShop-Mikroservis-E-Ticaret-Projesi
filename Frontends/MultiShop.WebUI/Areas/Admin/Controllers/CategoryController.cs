using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using System.Text;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/Category")]
public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICategoryService _categoryService;

    public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
    {
        _httpClientFactory = httpClientFactory;
        _categoryService = categoryService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Kategori Listesi";
        ViewBag.v0 = "Kategori İşlemleri";

        var values = await _categoryService.GetAllCategoryAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateCategory")]
    public IActionResult CreateCategory()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Kategori Listesi";
        ViewBag.v0 = "Kategori İşlemleri";

        return View();
    }
 
    [HttpPost]
    [Route("CreateCategory")]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        //var client = _httpClientFactory.CreateClient();
        //var jsonData = JsonConvert.SerializeObject(createCategoryDto);
        //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //var responseMessage = await client.PostAsync("http://localhost:7260/api/Categories", stringContent);

        await _categoryService.CreateCategoryAsync(createCategoryDto);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
        
    }

    [Route("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Kategori Listesi";
        ViewBag.v0 = "Kategori İşlemleri";

        var value = await _categoryService.GetByIdCategoryAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        await _categoryService.UpdateCategoryAsync(updateCategoryDto);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }
}
