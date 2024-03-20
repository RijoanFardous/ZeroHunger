using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.EF;
using ZeroHunger.DTOs;

namespace ZeroHunger.Controllers
{
    [RestaurantAccess]
    public class RestaurantController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.User = (User)Session["user"];

            ZeroHungerEntities db = new ZeroHungerEntities();
            var requests = db.CollectionRequests.ToList();
            return View(requests);
        }

        [HttpGet]
        public ActionResult CollectionRequest()
        {
            return View(new CollectionRequestDTO());
        }

        [HttpPost]
        public ActionResult CollectionRequest(CollectionRequestDTO cr)
        {
            if(ModelState.IsValid)
            {
                CollectionRequest req = new CollectionRequest();
                req.RequestedAt = DateTime.Now;
                req.RequestedBy = ((User)Session["user"]).Id;
                req.MaxPreserveTime = cr.MaxPreserveTime;
                req.Description = cr.Description;
                req.Status = "Pending";

                ZeroHungerEntities db = new ZeroHungerEntities();
                db.CollectionRequests.Add(req);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(cr);
        }

        public ActionResult Details(int id)
        {
            ZeroHungerEntities db = new ZeroHungerEntities();
            var request = db.CollectionRequests.Find(id);
            return View(request);
        }
    }
}