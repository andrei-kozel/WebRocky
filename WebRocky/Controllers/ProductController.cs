using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebRocky.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using WebRocky.Data;
using WebRocky.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace WebRocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objectList = _db.Product;

            foreach (var prod in objectList)
            {
                prod.Category = _db.Category.FirstOrDefault(u => u.Id == prod.CategoryId);
            }

            return View(objectList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                })
            };

            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Product.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string rootPath = _hostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    string upload = rootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;
                    _db.Product.Add(productVM.Product);

                }
                else
                {
                    // update product
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
