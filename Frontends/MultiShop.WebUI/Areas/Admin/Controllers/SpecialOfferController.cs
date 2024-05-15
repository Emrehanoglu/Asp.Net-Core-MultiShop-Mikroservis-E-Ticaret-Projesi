﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/SpecialOffer")]
public class SpecialOfferController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SpecialOfferController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7260/api/SpecialOffers");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    [Route("CreateSpecialOffer")]
    public IActionResult CreateSpecialOffer()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";
        return View();
    }

    [HttpPost]
    [Route("CreateSpecialOffer")]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createSpecialOfferDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PostAsync("https://localhost:7260/api/SpecialOffers", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
        return View();
    }

    [Route("DeleteSpecialOffer/{id}")]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"https://localhost:7260/api/SpecialOffers/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün Fırsatı";
        ViewBag.v0 = "Özel Teklif ve Günün Fırsatı İşlemleri";
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:7260/api/SpecialOffers/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);
            return View(value);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateSpecialOfferDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7260/api/SpecialOffers", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
        return View();
    }
}
