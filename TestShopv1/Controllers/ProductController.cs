using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestShopv1.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyContext _db;
        private readonly IWebHostEnvironment _webHostEnvirinment;
        //Ruft die IFileProvider-Schnittstelle ab, die auf die WebRootPath-Eigenschaft zeigt, oder legt diese fest. Dies verweist standardmäßig auf Dateien aus dem Unterordner "wwwroot". Ruft den absoluten Pfad zu dem Verzeichnis ab, das die webservierbaren Anwendungsinhaltsdateien enthält, oder legt diesen fest.
        public ProductController(MyContext myContext, IWebHostEnvironment webHostEnvirinment)
        {
            _db = myContext;
            _webHostEnvirinment = webHostEnvirinment;
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
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            ModelState.Remove("Product.Id");
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvirinment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImagePath))
                    {
                        //altes bild löschen wenn ein neues kommt
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImagePath = @"\images\product\" + fileName;

                }


                _db.Products.Add(productVM.Product);
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Da wurde was falsch eingegeben";
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
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid )
            {
                string wwwRootPath = _webHostEnvirinment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImagePath))
                    {
                        //altes bild löschen wenn ein neues kommt
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImagePath = @"\images\product\" + fileName;

                }


                _db.Products.Update(Services.Helper.PR_Mapping(productVM));
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

        #region Api
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> productList = _db.Products.Include(u => u.Category).Include(u => u.Manufacturer).ToList();
            var options = new JsonSerializerOptions
            {
                // Fügen Sie ReferenceHandler.Preserve hinzu, um Objektreferenzen zu erhalten
                ReferenceHandler = ReferenceHandler.Preserve,

                // Weitere Optionen können hier hinzugefügt werden
                //WriteIndented = true
            };
            #region test
            //var testProduct = new Product
            //{
            //    Id = 1,
            //    Name = "Test Product",
            //    Description = "This is a test product",
            //    Category = new Category { Id = 1, Name = "TestCategory" },
            //    Manufacturer = new Manufacturer { Id = 1, Name = "TestManufacturer" },
            //    UnitPriceNetto = 100.00m,
            //    ImagePath = "test.jpg"
            //};
            //return Json(new { data = testProduct }); 
            #endregion
            return Json(new { data = productList }, options);
        }
        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{
        //    var prodToDelete = _db.Products.FirstOrDefault(z => z.Id == id);
        //    if (prodToDelete == null)
        //    {
        //        return Json(new { success = false, message = "Fehler beim löschen" });
        //    }

        //    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImagePath.TrimStart('\\'));
        //    if (System.IO.File.Exists(oldImagePath))
        //    {
        //        System.IO.File.Delete(oldImagePath);
        //    }
        //    var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve};        
        //    //return Json(new { data = productList }, options);
        //}

        #endregion

    }
}
