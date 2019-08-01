using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Customer, Employee")]
        public ActionResult Created()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("Create", "Employee");
            }
            else if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Create", "Customer");
            }

            return View("Index");
        }
    }
}