using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestShopv1.Models;

namespace TestShopv1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyContext _db;
        public CategoryController(MyContext myContext)
        {
            _db = myContext;
        }

        public IActionResult Index()
        {
            var obj = _db.Categories.ToList();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.TaxRate.ToString())
                ModelState.AddModelError("name", "Der Steuersatz kann nicht ident mit dem Namen sein!");

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getcategoryfromdb = _db.Categories.FirstOrDefault(z => z.Id == id);

            if (getcategoryfromdb == null)
            {
                return NotFound();
            }
            return View(getcategoryfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.TaxRate.ToString())
                ModelState.AddModelError("name", "Der Steuersatz kann nicht ident mit dem Namen sein!");

            if (ModelState.IsValid  && !string.IsNullOrEmpty(obj.ToString()))
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getcategoryfromdb = _db.Categories.FirstOrDefault(z => z.Id == id);

            if (getcategoryfromdb == null)
            {
                return NotFound();
            }
            return View(getcategoryfromdb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
                return NotFound();

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
