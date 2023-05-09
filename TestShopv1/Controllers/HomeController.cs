﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestShopv1.Models;

namespace TestShopv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _db;

        public static string Name { get { return nameof(HomeController).Replace("Controller", ""); } }

        public HomeController(ILogger<HomeController> logger, MyContext myContext)
        {
            _logger = logger;
            _db = myContext;

        }

        public IActionResult Index()
        {
            var obj = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).ToList();
            return View(obj);           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
