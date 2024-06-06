using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents;

public class _ProductDetailFeatureComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductService _productService;

    public _ProductDetailFeatureComponentPartial(IHttpClientFactory httpClientFactory, IProductService productService)
    {
        _httpClientFactory = httpClientFactory;
        _productService = productService;
    }
    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
        var values = await _productService.GetByIdProductAsync(id);
        return View(values);
    }
}
