using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.CatalogServices;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService ProductService)
    {
        _productService = ProductService;
    }
    [HttpGet]
    public async Task<IActionResult> ProductList()
    {
        var values = await _productService.GetAllProductAsync();
        return Ok(values);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var value = await _productService.GetByIdProductAsync(id);
        return Ok(value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        await _productService.CreateProductAsync(createProductDto);
        return Ok("Product bilgisi eklendi.");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok("Product bilgisi silindi.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        await _productService.UpdateProductAsync(updateProductDto);
        return Ok("Product bilgisi güncellendi.");
    }
    [HttpGet("GetProductsWithCategory")]
    public async Task<IActionResult> GetProductsWithCategory()
    {
        var values = await _productService.GetProductsWithCategoryAsync();
        return Ok(values);
    }
    [HttpGet("GetProductsWithCategoryByCategoryId/{id}")]
    public async Task<IActionResult> GetProductsWithCategoryByCategoryId(string id)
    {
        var values = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
        return Ok(values);
    }
}