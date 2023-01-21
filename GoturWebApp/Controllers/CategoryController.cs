using GoturWebApp.Data;
using GoturWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoturWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly GoturDbContext db;

        public CategoryController(GoturDbContext _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //Burda password validation yapabilirsin, name yerine password de
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = db.Categories.Find(id);
            //var categoryFromDbFirst = db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = db.Categories.SingleOrDefault(u => u.Id == id);
            
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //Burda password validation yapabilirsin, name yerine password de
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Update(obj);
                db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = db.Categories.Find(id);
            //var categoryFromDbFirst = db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            
            db.Categories.Remove(obj);
            db.SaveChanges();
            TempData["success"] = "Category removed successfully!";
            return RedirectToAction("Index");
        }

    }
}
