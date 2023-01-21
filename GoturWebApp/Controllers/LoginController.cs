using Microsoft.AspNetCore.Mvc;
using GoturWebApp.Models;
using GoturWebApp.Data;
using GoturWebApp.Controllers;

namespace GoturWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly GoturDbContext db;

        public LoginController(GoturDbContext _db)
        {
            this.db = _db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer obj)
        {
            if (obj.Customer_Name != null && obj.CustomerPassword != null && obj.Address != null)
            {
                db.Customers.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Account created successfully!";
                HomeController.loginedID = obj.CustomerID;
                Basket newBasket = new Basket(obj.CustomerID);
                db.Basket.Add(newBasket);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "Error while creating an account.";
            return View(obj);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Customer obj)
        {
            var customerLogined = db.Customers.FirstOrDefault(x => x.Customer_Name == obj.Customer_Name && x.CustomerPassword == obj.CustomerPassword);
            if (customerLogined!=null)
            {
                TempData["success"] = "Logined successfully!";
                HomeController.loginedID = customerLogined.CustomerID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Wrong username or password. Try again.";
                return View(obj);
            }
            
        }

        public IActionResult Logout()
        {
            HomeController.loginedID = -1;
            return RedirectToAction("Index", "Home");
        }
    }
}
