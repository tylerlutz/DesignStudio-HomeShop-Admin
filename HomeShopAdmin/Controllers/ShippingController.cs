using System.Web.Mvc;
using BeyondThemes.BeyondAdmin.Models;
using System.Linq;

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
    }
}