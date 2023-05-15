using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;

namespace TestShopv1.Controllers
{

    public class CardController : Controller
    {
        public CardVM CardVM { get; set; }
        private readonly MyContext _db;
   public CardController(MyContext myContext)
    {
        _db = myContext;
      
    }
        public IActionResult Index()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            int userID = int.Parse(userId);

            CardVM = new()
            {
                CardList = _db.ShoppingCards.Include(x => x.Product).Include(x => x.OrderLines).Where(s => s.CustomerId == userID).ToList()
            };

            foreach (var cart in CardVM.CardList)
            {
                //CardVM.OrderLine.TotalPriceBrutto += (cart.UnitPrice * cart.Amount);
                CardVM.OrderTotal += (cart.UnitPrice * cart.Amount);
            }
            return View(CardVM);
        }

       
    }
}
