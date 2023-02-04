﻿using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using WebRocky.Data;
using WebRocky.Models;

namespace WebRocky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objectList = _db.Category;
            return View(objectList);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}