using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _FeatureDefaultComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFeatureService _featureService;

    public _FeatureDefaultComponentPartial(IHttpClientFactory httpClientFactory, IFeatureService featureService)
    {
        _httpClientFactory = httpClientFactory;
        _featureService = featureService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _featureService.GetAllFeatureAsync();
        return View(values);
    }
}
