using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _SpecialOfferComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISpecialOfferService _specialOfferService;

    public _SpecialOfferComponentPartial(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService)
    {
        _httpClientFactory = httpClientFactory;
        _specialOfferService = specialOfferService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _specialOfferService.GetAllSpecialOfferAsync();
        return View(values);
    }
}
