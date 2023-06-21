using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;


namespace TestShopv1.ViewComponents
{
    public class ShoppingCardViewComponent : ViewComponent
    {
        private readonly MyContext _db;
        public ShoppingCardViewComponent(MyContext myContext)
        {
            _db = myContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            int? userID = int.Parse(userId);

            if (userID != null)
            {

                if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                   HttpContext.Session.SetInt32(SD.SessionCart,
                    _db.ShoppingCards.Where(u => u.CustomerId == userID).Count());
                }

                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
