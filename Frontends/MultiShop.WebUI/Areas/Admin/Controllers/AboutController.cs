﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/About")]
public class AboutController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAboutService _aboutService;

    public AboutController(IHttpClientFactory httpClientFactory, IAboutService aboutService)
    {
        _httpClientFactory = httpClientFactory;
        _aboutService = aboutService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Hakkımda";
        ViewBag.v3 = "Hakkımda Listesi";
        ViewBag.v0 = "Hakkımda İşlemleri";

        var values = await _aboutService.GetAllAboutAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateAbout")]
    public IActionResult CreateAbout()
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Hakkımda";
        ViewBag.v3 = "Hakkımda Listesi";
        ViewBag.v0 = "Hakkımda İşlemleri";

        return View();
    }

    [HttpPost]
    [Route("CreateAbout")]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        await _aboutService.CreateAboutAsync(createAboutDto);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }

    [Route("DeleteAbout/{id}")]
    public async Task<IActionResult> DeleteAbout(string id)
    {
        await _aboutService.DeleteAboutAsync(id);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateAbout/{id}")]
    public async Task<IActionResult> UpdateAbout(string id)
    {
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Hakkımda";
        ViewBag.v3 = "Hakkımda Listesi";
        ViewBag.v0 = "Hakkımda İşlemleri";

        var value = await _aboutService.GetByIdAboutAsync(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateAbout/{id}")]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        await _aboutService.UpdateAboutAsync(updateAboutDto);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }
}
