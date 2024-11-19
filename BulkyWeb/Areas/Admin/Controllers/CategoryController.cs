using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public readonly ICategoryRepository Db;

        public CategoryController(ICategoryRepository db)
        {
            Db = db;
        }
        public IActionResult Index()
        {
            List<Category> obj = Db.GetAll().ToList();
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                Db.Add(Category);
                Db.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category? Category = Db.GetById(u => u.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }
        [HttpPost]
        public IActionResult Edit(Category Category)
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                Db.Update(Category);
                Db.save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category? Category = Db.GetById(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Category Category = Db.GetById(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            Db.Remove(Category);
            Db.save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");

        }
    }
}
