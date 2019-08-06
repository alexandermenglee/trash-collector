using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using Microsoft.AspNet.Identity;

namespace TrashCollector.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        ApplicationDbContext _context;
        List<string> days; 

        public CustomerController()
        {
            _context = new ApplicationDbContext();
            days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        }
        // GET: Customer
        public ActionResult Index(int? id)
        {
            if(id != null)
            {
                Customer foundCustomer;

                foundCustomer = _context.Customers.Find(id);

                return View(foundCustomer);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                Customer customer = _context.Customers.Find(id);
                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.days = days;

            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                customer.ApplicationUserId = User.Identity.GetUserId();
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id != null)
            {
                Customer customer = _context.Customers.Find(id);

                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                Customer foundCustomer = _context.Customers.Find(customer.CustomerId);

                foundCustomer.FirstName = customer.FirstName;
                foundCustomer.LastName = customer.LastName;
                foundCustomer.Street = customer.Street;
                foundCustomer.City = customer.City;
                foundCustomer.State = customer.State;
                foundCustomer.Zip = customer.Zip;
                foundCustomer.PickUpDay = customer.PickUpDay;

                _context.SaveChanges();

                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET
        public ActionResult EditPickUpDay(int? id)
        {
            if(id != null)
            {
                Customer customer = _context.Customers.Find(id);
                ViewBag.days = days;

                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST
        [HttpPost]
        public ActionResult EditPickUpDay(Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                Customer foundCustomer = _context.Customers.Find(customer.CustomerId);

                foundCustomer.PickUpDay = customer.PickUpDay;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET
        [HttpGet]
        public ActionResult ShowBill(int? id)
        {
            DateTime currentDate = DateTime.Now;

            ViewBag.currentMonth = currentDate.ToString("MMMM"); ;

            if (id != null)
            {
                Customer customer = _context.Customers.Find(id);

                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/CreateSpecialPickupDate
        public ActionResult CreateSpecialPickupDate(int? id)
        {
            if(id != null)
            {
                Customer customer = _context.Customers.Find(id);
                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Customer/CreateSpecialPickupDate
        [HttpPost]
        public ActionResult CreateSpecialPickupDate(Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                Customer foundCustomer = _context.Customers.Find(customer.CustomerId);

                foundCustomer.SpecialPickupDate = customer.SpecialPickupDate;

                _context.SaveChanges();

                return RedirectToAction("Index", new { id = foundCustomer.CustomerId });
            }
            catch
            {
                return RedirectToAction("Index", "Home"); ;
            }
        }

        // GET: Customer/SuspendPickup
        public ActionResult SuspendPickup(int? id)
        {
            if (id != null)
            {
                Customer customer = _context.Customers.Find(id);
                return View(customer);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST Customer/SuspendPickup
        [HttpPost]
        public ActionResult SuspendPickup(Customer customer)
        {
            Customer foundCustomer;

            foundCustomer = _context.Customers.Find(customer.CustomerId);

            foundCustomer.StartSuspension = customer.StartSuspension;
            foundCustomer.EndSuspendsion = customer.EndSuspendsion;

            // Suspension: True
                // Now > StartSuspension and Now < EndSuspension
                // startOfSuspension > 0 && endOfSuspension < 0
            // Suspension: False
                // startOfSuspension < 0 || endOfSuspension > 0

            int startOfSuspension = DateTime.Compare(DateTime.Now, foundCustomer.StartSuspension.Value);
            int endOfSuspension = DateTime.Compare(DateTime.Now, foundCustomer.EndSuspendsion.Value);

            if(startOfSuspension > 0 && endOfSuspension < 0)
            {
                foundCustomer.AccountSuspended = true;
            }
            else if(startOfSuspension < 0 || endOfSuspension > 0)
            {
                foundCustomer.AccountSuspended = false;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", new { id = foundCustomer.CustomerId });
        }
    }
}
