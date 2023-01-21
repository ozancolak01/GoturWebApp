using GoturWebApp.Data;
using GoturWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoturWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly GoturDbContext db;
        public static int loginedID = -1;

        public HomeController(GoturDbContext _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = db.Products.ToList();
            if (loginedID != -1)
            {
                Customer? loginedCustomer = db.Customers.FirstOrDefault(c => c.CustomerID == loginedID);
                if (loginedCustomer != null)
                {
                    ViewBag.Name = loginedCustomer.Customer_Name;
                }
            }
            return View(productList);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}