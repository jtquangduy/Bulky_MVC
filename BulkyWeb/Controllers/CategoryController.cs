﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _db = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.GetAll().ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            /*
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            */

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Get(c => c.Id == id);
            /*Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Get(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}