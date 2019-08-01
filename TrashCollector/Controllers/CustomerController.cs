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

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
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
                // street city state zip
                foundCustomer.Street = customer.Street;
                foundCustomer.City = customer.City;
                foundCustomer.State = customer.State;
                foundCustomer.Zip = customer.Zip;

                _context.SaveChanges();

                return RedirectToAction("Index");
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
                List<string> days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
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
    }
}
