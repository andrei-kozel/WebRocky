using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebRocky.Data;
using WebRocky.Models;

namespace WebRocky.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Task> objList = _db.Task;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task obj)
        {
            _db.Task.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
