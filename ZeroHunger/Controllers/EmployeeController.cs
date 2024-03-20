using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    [EmployeeAccess]
    public class EmployeeController : Controller
    {
        ZeroHungerEntities db = new ZeroHungerEntities();

        public ActionResult Index()
        {
            ViewBag.User = (User)Session["user"];

            ZeroHungerEntities db = new ZeroHungerEntities();
            var distributions = db.Distributions.ToList();
            return View(distributions);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var request = db.CollectionRequests.Find(id);
            return View(request);
        }

        [HttpPost]
        public ActionResult Details(DistributionDTO d)
        {
            var request = db.CollectionRequests.Find(d.RequestId);
            request.Status = "Completed";
            db.SaveChanges();

            var distribution = db.Distributions.Find(d.Id);
            distribution.Status = "Completed";
            distribution.DistributedAt = DateTime.Now;
            distribution.PeopleFed = d.PeopleFed;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}