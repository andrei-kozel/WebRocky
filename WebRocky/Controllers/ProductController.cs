using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebRocky.Data;
using WebRocky.Models;

namespace WebRocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
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
            IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
            Product product = new Product();

            ViewBag.CategoryDropDown = CategoryDropDown;

            if (id == null)
            {
                return View(product);
            }
            else
            {
                product = _db.Product.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

    }
}
