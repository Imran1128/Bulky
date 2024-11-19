using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IProductRepository Db;
        private readonly ICategoryRepository category;

        public ProductController(IProductRepository db, ICategoryRepository category)
        {
            Db = db;
            this.category = category;
        }
        public IActionResult Index()
        {
            List<Product> obj = Db.GetAll().ToList();
            return View(obj);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }
                );
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product Product)
        {
           
            if (ModelState.IsValid)
            {
                Db.Add(Product);
                Db.save();
                TempData["success"] = "Product created successfully";
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
            Product? Product = Db.GetById(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost]
        public IActionResult Edit(Product Product)
        {
          
            if (ModelState.IsValid)
            {
                Db.Update(Product);
                Db.save();
                TempData["success"] = "Product updated successfully";
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
            Product? Product = Db.GetById(c => c.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Product Product = Db.GetById(c => c.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            Db.Remove(Product);
            Db.save();
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");

        }
    }
}
