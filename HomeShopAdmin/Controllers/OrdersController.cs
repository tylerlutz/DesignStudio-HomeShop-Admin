using BeyondThemes.BeyondAdmin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondThemes.BeyondAdmin.Controllers
{
    public class OrdersController : Controller
    {
        HomeStoreEntities db = new HomeStoreEntities();

        public ActionResult Index()
        {
            var orders = db.CustomerOrders;

            return View(orders.OrderByDescending(o => o.OrderID));
        }

        public ActionResult Details(int? id)
        {
            OrderDetails detail = new OrderDetails();

            CustomerOrder order = db.CustomerOrders.Find(id);
            detail.Order = order;

            var items = db.ShoppingCartItems.Where(i => i.OrderID == order.OrderID);

            foreach(ShoppingCartItem item in items)
            {
                detail.Items.Add(item);
            }

            return View(detail);
        }
    }
}