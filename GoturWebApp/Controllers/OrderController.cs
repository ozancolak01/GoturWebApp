using GoturWebApp.Data;
using GoturWebApp.Models;
using GoturWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoturWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly GoturDbContext db;

        public OrderController(GoturDbContext _db)
        {
            this.db = _db;
        }

        public IActionResult Orders()
        {
            var customer = db.Customers.Find(HomeController.loginedID);
            var orderList = (from o in db.Orders
                              join c in db.Customers
                              on o.CustomerID equals c.CustomerID
                              select new ViewModelOrder
                              {
                                  OrderID = o.OrderID,
                                  OrderDate = o.DateOrdered,
                                  CustomerID = c.CustomerID,
                              });
            return View(orderList.Where(l => l.CustomerID == HomeController.loginedID));
        }

        public IActionResult OrderDetails(int id)
        {
            var order = db.Orders.Find(id);
            var orderDetails = (from od in db.OrderDetails
                             join o in db.Orders
                             on od.OrderID equals o.OrderID
                             join p in db.Products on od.ProductID equals p.ProductID
                             select new ViewModelOrderDetails
                             {
                                 OrderID = o.OrderID,
                                 OrderDetailsID = od.OrderDetailsID,
                                 ProductID = p.ProductID,
                                 ProductName = p.Name,
                                 ProductPrice = p.Price,
                                 ProductPhoto = p.PhotoLink
                             });
            return View(orderDetails.Where(l=> l.OrderID == id));
        }

        public IActionResult Delete(int id)
        {
            Order? orderRef = db.Orders.FirstOrDefault(o => o.CustomerID == HomeController.loginedID);

            if (orderRef == null)
            {
                TempData["error"] = "Order could not be found.";
                return RedirectToAction("Orders");
            }
            db.Orders.Remove(orderRef);
            db.SaveChanges();
            TempData["success"] = "Order removed successfully!";
            return RedirectToAction("Orders");
        }

        public IActionResult Order(int id)
        {
            var basket = db.Basket.Find(id);
            return View(basket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(Basket obj)
        {
            if (HomeController.loginedID == -1)
            {
                TempData["error"] = "You have to login first.";
                return RedirectToAction("Login", "Login");
            }

            Order newOrder = new Order(HomeController.loginedID, "ordered");
            db.Orders.Add(newOrder);
            db.SaveChanges();

            var basketList = (from b in db.Basket 
                              join c in db.Customers on b.CustomerID equals c.CustomerID
                              join o in db.Orders on c.CustomerID equals o.CustomerID
                              join bd in db.BasketDetail on b.BasketID equals bd.BasketID
                              join p in db.Products on bd.ProductID equals p.ProductID
                              select new{
                                  OrderID = o.OrderID,
                                  CustomerID = c.CustomerID,
                                  ProductID = p.ProductID,
                              });

            foreach (var l in basketList.Where(l => l.OrderID == newOrder.OrderID))
            {
                OrderDetail newDetail = new OrderDetail(newOrder.OrderID, l.ProductID);
                db.OrderDetails.Add(newDetail);
            }
            db.BasketDetail.RemoveRange(db.BasketDetail);
            db.SaveChanges();

            TempData["success"] = "Ordered successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
