using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeyondThemes.BeyondAdmin.Models;
using PagedList;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace BeyondThemes.BeyondAdmin.Controllers
{
    public class ProductsController : Controller
    {
        HomeStoreEntities db = new HomeStoreEntities();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var products = from p in db.Products select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString) || p.ProductDesc.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "category":
                    products = products.OrderBy(p => p.Category);
                    break;
                case "cat_desc":
                    products = products.OrderByDescending(p => p.Category);
                    break;
                case "quantity":
                    products = products.OrderBy(p => p.Quantity);
                    break;
                case "quant_desc":
                    products = products.OrderByDescending(p => p.Quantity);
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }
        // GET : product details
        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET : product/create
        public ActionResult Create()
        {
            return View();
        }

        // POST : product/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create ([Bind(Include = "ProductName, ProductDesc, Category, Quantity, Price")]Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                // log the error
                ModelState.AddModelError("", "Unable to save changes at this time. If the error persists, see your system's administrator, and beg forgiveness.");
            }
            return View(product);
        }

        // GET : product Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST : product edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productUpdate = db.Products.Find(id);
            if (TryUpdateModel(productUpdate, "", new string [] { "ProductName, ProductDesc, Category, Quantity, Price" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes at this time. If the error persists, see your system's administrator, and beg forgiveness.");
                }
            }
            return View(productUpdate);
        }
    }
} 