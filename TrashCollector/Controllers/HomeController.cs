using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
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

        [Authorize(Roles = "Customer, Employee")]
        public ActionResult RedirectToIndex()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("Index", "Employee");
            }
            else if (User.IsInRole("Customer"))
            {
                Customer customer;
                string currentSignedInCustomerApplicationId;

                currentSignedInCustomerApplicationId = User.Identity.GetUserId();
                customer = _context.Customers.Where(c => c.ApplicationUserId.Equals(currentSignedInCustomerApplicationId)).Single();

                return RedirectToAction("Index", "Customer", new { id = customer.CustomerId });
            }

            return View("Index");
        }
    }
}