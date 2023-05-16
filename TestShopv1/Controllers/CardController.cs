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
            NettoBruttoTotal_Rechner(CardVM.CardList);
                

            return View(CardVM);
        }

        public IActionResult Plus(int cartId)
        {
            var cartfromDb = _db.ShoppingCards.FirstOrDefault(q => q.Id == cartId);
            cartfromDb.Amount += 1;
            _db.ShoppingCards.Update(cartfromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartfromDb = _db.ShoppingCards.FirstOrDefault(q => q.Id == cartId);
            if (cartfromDb.Amount <=1)
            {
                _db.ShoppingCards.Remove(cartfromDb);
            }
            else
            {
                cartfromDb.Amount -= 1;
                _db.ShoppingCards.Update(cartfromDb); 
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cartfromDb = _db.ShoppingCards.FirstOrDefault(q => q.Id == cartId);
            _db.ShoppingCards.Remove(cartfromDb);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public decimal? NettoBruttoTotal_Rechner(List<ShoppingCard> cards)
        {

            decimal tax02 = 0.0m;
            decimal tax01 = 0.0m;
            decimal unitgesamt = 0.0m;
            foreach (var item in cards)
            {
                unitgesamt += (item.Product.UnitPriceNetto * item.Amount);
                if (item.Product.Category.TaxRate == 0.20m)
                {
                    tax02 += (decimal)(unitgesamt * item.Product.Category.TaxRate);
                    CardVM.OrderTotal += unitgesamt + tax02;
                } 
                else if (item.Product.Category.TaxRate == 0.10m)
                {
                    tax01 += (decimal)(unitgesamt * item.Product.Category.TaxRate);
                    CardVM.OrderTotal += unitgesamt + tax01;
                }
            }

            return CardVM.OrderTotal;
        }

        public IActionResult Summary()
        {

            return View();
        }
    }
}
