using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductDetailsController : ControllerBase
{
    private readonly IProductDetailService _productDetailservice;

    public ProductDetailsController(IProductDetailService ProductDetailservice)
    {
        _productDetailservice = ProductDetailservice;
    }
    [HttpGet]
    public async Task<IActionResult> ProductDetailList()
    {
        var values = await _productDetailservice.GetAllProductDetailAsync();
        return Ok(values);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailById(string id)
    {
        var value = await _productDetailservice.GetByIdProductDetailAsync(id);
        return Ok(value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
    {
        await _productDetailservice.CreateProductDetailAsync(createProductDetailDto);
        return Ok("Product Detail bilgisi eklendi.");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        await _productDetailservice.DeleteProductDetailAsync(id);
        return Ok("Product Detail bilgisi silindi.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
    {
        await _productDetailservice.UpdateProductDetailAsync(updateProductDetailDto);
        return Ok("Product Detail bilgisi güncellendi.");
    }
}