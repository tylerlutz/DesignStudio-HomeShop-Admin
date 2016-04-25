using System.Web.Mvc;
using BeyondThemes.BeyondAdmin.Models;
using System.Linq;
using System.Net;
using System.Data.Entity;

namespace BeyondThemes.BeyondAdmin.Controllers
{
    [Authorize]
    public class ShippingController : Controller
    {

        HomeStoreEntities db = new HomeStoreEntities();
        
        public ActionResult Index()
        {           
            return View(db.ShippingTypes.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingName,ShippingCost")] ShippingType shipType)
        {
            if (ModelState.IsValid)
            {
                db.ShippingTypes.Add(shipType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipType);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ShippingType shipType = db.ShippingTypes.Find(id);

            if(shipType == null)
            {
                return HttpNotFound();
            }

            return View(shipType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingID,ShippingName,ShippingCost")]ShippingType shipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipType);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ShippingType shipType = db.ShippingTypes.Find(id);

            if(shipType == null)
            {
                return HttpNotFound();
            }

            return View(shipType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShippingType shipType = db.ShippingTypes.Find(id);
            db.ShippingTypes.Remove(shipType);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}