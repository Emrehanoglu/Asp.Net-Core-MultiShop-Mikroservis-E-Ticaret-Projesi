using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageservice;

    public ProductImagesController(IProductImageService ProductImageservice)
    {
        _productImageservice = ProductImageservice;
    }
    [HttpGet]
    public async Task<IActionResult> ProductImageList()
    {
        var values = await _productImageservice.GetAllProductImageAsync();
        return Ok(values);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImageById(string id)
    {
        var value = await _productImageservice.GetByIdProductImageAsync(id);
        return Ok(value);
    }
    [HttpGet("ProductImageByProductIdAsync/{id}")]
    public async Task<IActionResult> ProductImageByProductIdAsync(string id)
    {
        var value = await _productImageservice.GetProductImageByProductIdAsync(id);
        return Ok(value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
    {
        await _productImageservice.CreateProductImageAsync(createProductImageDto);
        return Ok("Product Image bilgisi eklendi.");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        await _productImageservice.DeleteProductImageAsync(id);
        return Ok("Product Image bilgisi silindi.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
    {
        await _productImageservice.UpdateProductImageAsync(updateProductImageDto);
        return Ok("Product Image bilgisi güncellendi.");
    }
}