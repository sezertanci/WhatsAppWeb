﻿using Microsoft.AspNetCore.Mvc;

namespace WebAppMvcJquery.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
