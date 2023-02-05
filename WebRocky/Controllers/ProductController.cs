using Microsoft.AspNetCore.Mvc;
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

    }
}
