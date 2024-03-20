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
    [AdminAccess]
    public class AdminController : Controller
    {
        ZeroHungerEntities db = new ZeroHungerEntities();

        public ActionResult Index()
        {
            var requests = db.CollectionRequests.ToList();
            return View(requests);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var employees = (from u in db.Users where u.Type.Equals("Employee") select u).ToList();
            ViewBag.Employees = employees;

            var request = db.CollectionRequests.Find(id);
            return View(request);
        }

        [HttpPost]
        public ActionResult Details(DistributionDTO d)
        {
            
            var request = db.CollectionRequests.Find(d.RequestId);

            if (ModelState.IsValid)
            {
                request.Status = "Approved";
                db.SaveChanges();

                Distribution dis = new Distribution();
                dis.RequestId = d.RequestId;
                dis.EmployeeId = d.EmployeeId;
                dis.PeopleFed = 0;
                dis.Status = "Created";
                db.Distributions.Add(dis);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var employees = (from u in db.Users where u.Type.Equals("Employee") select u).ToList();
            ViewBag.Employees = employees;

            
            return View(request);
        }
    }
}