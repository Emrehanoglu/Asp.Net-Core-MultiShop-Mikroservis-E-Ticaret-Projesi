﻿using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
