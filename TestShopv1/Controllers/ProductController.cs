using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;

namespace TestShopv1.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyContext _db;
        public ProductController(MyContext myContext)
        {
            _db = myContext;
        }

        public IActionResult Index()
        {
            var obj = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).ToList();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM();
            productVM.CategoryList = _db.Categories.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            productVM.ManufacturList = _db.Manufacturers.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {

            if (ModelState.IsValid)
            {
                _db.Products.Add(productVM.Product);
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _db.Categories.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                productVM.ManufacturList = _db.Manufacturers.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _db.Categories.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                ManufacturList = _db.Manufacturers.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }
            productVM.Product = _db.Products.FirstOrDefault(u => u.Id == id);
           // productVM.Product.UnitPriceNetto =  productVM.Product.UnitPriceNetto.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {

            if (ModelState.IsValid )
            {
                _db.Products.Update(productVM.Product);
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getItemfromdb = _db.Products.FirstOrDefault(z => z.Id == id);

            if (getItemfromdb == null)
            {
                return NotFound();
            }
            return View(getItemfromdb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Products.Find(id);
            if (obj == null)
                return NotFound();

            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product delete successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
