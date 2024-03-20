using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;
using ZeroHunger.DTOs;
using System.Web.Helpers;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            User user = (User)Session["user"];

            if(user != null)
            {
                return RedirectToAction("Index", user.Type);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            ZeroHungerEntities db = new ZeroHungerEntities();
            User user = (from u in db.Users where u.Email.Equals(email) && u.Password.Equals(password) select u).SingleOrDefault();

            if (user != null)
            {
                Session["user"] = user;
                return RedirectToAction("Index", ((User)Session["user"]).Type);
            }

            ViewBag.LoginError = "Incorrect Email/Password.";

            return View(new UserDTO
            {
                Email = email,
                Password = password
            });
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public ActionResult Signup(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                ZeroHungerEntities db = new ZeroHungerEntities();
                User newUser = Convert(user);
                db.Users.Add(newUser);
                db.SaveChanges();

                Session["user"] = newUser;
                return RedirectToAction("Index", newUser.Type);
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login");
        }

        public static User Convert(UserDTO user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Type = user.Type,
                Address = user.Address,
            };
        }
    }
}