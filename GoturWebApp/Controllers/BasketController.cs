using Microsoft.AspNetCore.Mvc;
using GoturWebApp.Models;
using GoturWebApp.Models.ViewModels;
using GoturWebApp.Data;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GoturWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly GoturDbContext db;

        public BasketController(GoturDbContext _db)
        {
            this.db = _db;
        }

        public IActionResult Basket()
        {
            if (HomeController.loginedID == -1)
            {
                TempData["error"] = "You have to login first.";
                return RedirectToAction("Login", "Login");
            }

            var basketList = (from b in db.Basket
                             join c in db.Customers
                             on b.CustomerID equals c.CustomerID
                             join bd in db.BasketDetail on b.BasketID equals bd.BasketID
                             join p in db.Products on bd.ProductID equals p.ProductID
                             select new ViewModelBasket
                             {
                                 BasketID = b.BasketID,
                                 CustomerID = c.CustomerID,
                                 BasketDetailsID = bd.BasketDetailId,
                                 ProductID = p.ProductID,
                                 ProductName = p.Name,
                                 ProductPrice = p.Price,
                                 ProductPhoto = p.PhotoLink
                             });

            return View(basketList);
        }

        public IActionResult AddToBasket(int id)
        {
            var addedProduct = db.Products.Find(id);
            //Product? addedProduct = db.Products.FirstOrDefault(p => p.ProductID == id);
            return View(addedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToBasket(Product obj)
        {
            if(HomeController.loginedID == -1)
            {
                TempData["error"] = "You have to login first.";
                return RedirectToAction("Login", "Login");
            }
            var customerRef = db.Customers.Find(HomeController.loginedID);
            //Customer? customerRef = db.Customers.FirstOrDefault(p => p.CustomerID == HomeController.loginedID);
            Basket? basketRef = db.Basket.FirstOrDefault(b => b.CustomerID == customerRef.CustomerID);
            BasketDetail newDetail = new BasketDetail(basketRef.BasketID, obj.ProductID);

            db.BasketDetail.Add(newDetail);
            db.SaveChanges();
            

            TempData["success"] = "Product added to basket.";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            Basket? basketRef = db.Basket.FirstOrDefault(b => b.CustomerID == HomeController.loginedID);
            BasketDetail? deleteDetail = db.BasketDetail.FirstOrDefault(bd => bd.BasketID == basketRef.BasketID && bd.ProductID == id);

            if (deleteDetail == null)
            {
                TempData["error"] = "Product could not be deleted.";
                return RedirectToAction("Basket");
            }
            db.BasketDetail.Remove(deleteDetail);
            db.SaveChanges();
            TempData["success"] = "Product removed from basket successfully!";
            return RedirectToAction("Basket");
        }

        public IActionResult Details(int id)
        {
            Product? addedProduct = db.Products.FirstOrDefault(p => p.ProductID == id);
            return View(addedProduct);
        }
    }
}
