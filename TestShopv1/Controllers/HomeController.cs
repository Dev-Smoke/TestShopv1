using Abp.Web.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;

namespace TestShopv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _db;
        public static int? catId;
        public static int? manuId;

        public static string Name { get { return nameof(HomeController).Replace("Controller", ""); } }

        public HomeController(ILogger<HomeController> logger, MyContext myContext)
        {
            _logger = logger;
            _db = myContext;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var obj = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).ToList();
            return View(obj);
        }
        [HttpPost, ActionName("Index")]
        public IActionResult IndexPost(string? search, int? categoryid, int? manufacturid)
        {
            //eINE vIEW ERSTELLN FÜR CATEGORYS UND HERSTELLER; und da kann ich mit der jeweiligen id davon von cat oder manu suchen und bleider in der drinnen
            var obj = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                if (catId != null)
                {
                    obj = obj.Where(s => s.CategoryId == catId && s.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Manufacturer.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Description.ToLowerInvariant().Contains(search.ToLowerInvariant())).ToList();
                }
                else if (manuId != null)
                {
                    obj = obj.Where(s => s.ManufacturerId == manuId && s.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Manufacturer.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Description.ToLowerInvariant().Contains(search.ToLowerInvariant())).ToList();
                }
                else
                {
                    obj = obj.Where(s => s.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Manufacturer.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                    s.Description.ToLowerInvariant().Contains(search.ToLowerInvariant())).ToList();
                }

            }
            if (categoryid != null)
            {
                catId = categoryid;
                obj = obj.Where(x => x.CategoryId == categoryid).ToList();
            }
            if (manufacturid != null)
            {
                manuId = manufacturid;
                obj = obj.Where(x => x.ManufacturerId == manufacturid).ToList();
            }
            return View(obj);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCard cart = new()
            {
                Product = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).FirstOrDefault(p => p.Id == productId),
                Amount = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(ShoppingCard shoppingCard)
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
             shoppingCard.CustomerId = int.Parse(userId);
            int userID = int.Parse(userId);

            ShoppingCard? cardFromDb = _db.ShoppingCards.FirstOrDefault( s => s.CustomerId == userID && s.ProductId == shoppingCard.ProductId);

            if (cardFromDb !=  null)
            {
                //existiert schon 
                cardFromDb.Amount += shoppingCard.Amount;
                _db.ShoppingCards.Update(cardFromDb);
                _db.SaveChanges();
            }
            else
            {
                _db.ShoppingCards.Add(shoppingCard);
                _db.SaveChanges();
                //Session
                HttpContext.Session.SetInt32(SD.SessionCart, _db.ShoppingCards.Where(u => u.CustomerId == userID)
                    .Count());
            }
            TempData["success"] = "Cart updated successfully";

            _db.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
