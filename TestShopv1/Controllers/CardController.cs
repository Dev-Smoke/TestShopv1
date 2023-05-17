using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;

namespace TestShopv1.Controllers
{

    public class CardController : Controller
    {
        [BindProperty]
        public CardVM CardVM { get; set; }
        public static OrderLine OrderLineTemp { get; set; }
        public static decimal? TotalPrice { get; set; }
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
                CardList = _db.ShoppingCards.Include(x => x.Product).Include(x => x.Category).Where(s => s.CustomerId == userID).ToList(),
                Order = new()
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
            if (cartfromDb.Amount <= 1)
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
            decimal tax = 0.0m;
            decimal unitgesamt = 0.0m;
            decimal unitgesamtNet = 0.0m;
            var catL = _db.Categories.ToList();
            foreach (var item in cards)
            {
                unitgesamt = (item.Product.UnitPriceNetto * item.Amount);
                unitgesamtNet += unitgesamt;
                if (catL.FirstOrDefault(x => x.Id == item.Product.CategoryId).TaxRate == 0.20m)
                {
                    tax02 = (decimal)((item.Product.UnitPriceNetto * item.Amount) * item.Product.Category.TaxRate);
                    tax += tax02;
                    CardVM.OrderTotal += (unitgesamt + tax02);
                }
                #region 10%

                //else if (catL.FirstOrDefault(x => x.Id == item.Product.CategoryId).TaxRate == 0.10m)
                //{
                //    tax01 = (decimal)((item.Product.UnitPriceNetto * item.Amount) * item.Product.Category.TaxRate);
                //    CardVM.OrderTotal += unitgesamt + tax01;
                //}

                #endregion

                TempData["Steuer"] = tax.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));
                TempData["Netto"] = unitgesamtNet.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));
                CardVM.Order.TotalPriceBrutto = CardVM.OrderTotal;
                CardVM.Order.BruttoPreis = (decimal)CardVM.Order.TotalPriceBrutto;
            }

            return CardVM.OrderTotal;
        }

        public IActionResult Summary()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            int userID = int.Parse(userId);

            CardVM = new()
            {
                CardList = _db.ShoppingCards.Include(x => x.Product).Include(x => x.Category).Include(x => x.Customer).Where(s => s.CustomerId == userID).ToList(),
                Order = new()
            };
            CardVM.Order.Customer = _db.Customers.FirstOrDefault(x => x.Id == userID);

            CardVM.Order.RecipientSalutation = CardVM.Order.Customer.Salutation;
            CardVM.Order.RecipientFirstname = CardVM.Order.Customer.Firstname;
            CardVM.Order.RecipientLastname = CardVM.Order.Customer.Lastname;
            CardVM.Order.RecipientStreet = CardVM.Order.Customer.Street;
            CardVM.Order.RecipientCity = CardVM.Order.Customer.City;
            CardVM.Order.RecipientZipCode = CardVM.Order.Customer.ZipCode;

            NettoBruttoTotal_Rechner(CardVM.CardList);

            return View(CardVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            OrderLineTemp = null;
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            int userID = int.Parse(userId);
            CardVM.CardList = _db.ShoppingCards.Include(x => x.Product).Include(x => x.Category).Include(x => x.Customer).Where(s => s.CustomerId == userID).ToList();
 
            TotalPrice = NettoBruttoTotal_Rechner(CardVM.CardList);
    
            CardVM.Order.OrderedOn = DateTime.Now;
            CardVM.Order.CustomerId = userID;

            if (CardVM.Order.AlternativeLieferaddresse != null)
                CardVM.Order.RecipientStreet = CardVM.Order.AlternativeLieferaddresse;

            _db.Orders.Add(CardVM.Order);
            _db.SaveChanges();

            foreach (var item in CardVM.CardList)
            {
                OrderLine orderLine = new()
                {
                    ProductId = item.Product.Id,
                    OrderId = CardVM.Order.Id,
                    TotalPriceBrutto = ((item.Product.UnitPriceNetto * item.Amount) * (1.0m + _db.Categories.FirstOrDefault(x => x.Id == item.Product.CategoryId).TaxRate)),
                    Quantity = item.Amount,
                    TaxRate = _db.Categories.FirstOrDefault(x => x.Id ==  item.Product.CategoryId).TaxRate ,
                    ShoppingCardId = item.Id
                };
                       OrderLineTemp = orderLine;
                _db.OrderLines.Add(orderLine);
                _db.SaveChanges();
            }
            
            return RedirectToAction(nameof(OrderConfirmation));
        }

        public IActionResult OrderConfirmation()
        {


            OrderLine orderLine = OrderLineTemp;
            orderLine.TotalPriceBrutto = TotalPrice;

            return View(orderLine);
        }
    }
}
