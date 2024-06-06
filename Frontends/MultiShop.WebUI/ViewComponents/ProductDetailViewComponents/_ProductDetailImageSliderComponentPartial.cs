using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImagesServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents;

public class _ProductDetailImageSliderComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductImageService _productImageService;

    public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory, IProductImageService productImageService)
    {
        _httpClientFactory = httpClientFactory;
        _productImageService = productImageService;
    }
    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
        var values = await _productImageService.GetProductImageByProductIdAsync(id);
        return View(values);
    }
}
