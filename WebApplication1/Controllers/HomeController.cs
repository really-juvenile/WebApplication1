using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;
using OneToOneStudentDemo.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var user = new User //property initialization syntax bro
                    {
                        Username = username,
                        Password = hashedPassword
                    };
                    session.Save(user);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    var user = session.Query<User>().FirstOrDefault(u=>u.Username == username);

                    if(user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                    { 
                        return RedirectToAction("Index");
                    }

                   
                }
                ViewBag.ErrorMessage = "Invalid Username or Password";
                return View();
            }
            
        }

        public ActionResult Index()
        {
            return Content("Welcome bro BCrypt is working");
        }
        



        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    // You can add code here to verify username and password (e.g., using a database).
        //    if (username == "admin" && password == "admin") // Dummy check
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    //if (ModelState.IsValid)
        //    //{


        //    //    // If login fails, add an error message

        //    //}

        //    return View();
        //}
    }
}