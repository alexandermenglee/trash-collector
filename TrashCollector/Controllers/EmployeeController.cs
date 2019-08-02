using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using Microsoft.AspNet.Identity;

namespace TrashCollector.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        ApplicationDbContext _context;
        List<string> days;
        public EmployeeController()
        {
            _context = new ApplicationDbContext();
            days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        }
        // GET: Employee
        public ActionResult Index()
        {
            string signedInEmployeeApplicationId;
            Employee signedInEmployee;
            string today;
            IQueryable<Customer> customers;
            ViewBag.days = days;


            signedInEmployeeApplicationId = User.Identity.GetUserId();
            signedInEmployee = _context.Employees.Where(e => e.ApplicationUserId.Equals(signedInEmployeeApplicationId)).Single();
            today = DateTime.Today.DayOfWeek.ToString();
            customers = _context.Customers.Where(c => c.Zip == signedInEmployee.Zip && c.PickUpDay.Equals(today));

            return View(customers);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                Employee employee = _context.Employees.Find(id);

                return View(employee);
            }
                       
            return RedirectToAction("Index", "Home");
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here
                employee.ApplicationUserId = User.Identity.GetUserId();
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Employee employee = _context.Employees.Where(e => e.EmployeeId == id).Single();

                return View(employee);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                Employee foundEmployee = _context.Employees.Find(employee.EmployeeId);

                foundEmployee.FirstName = employee.FirstName;
                foundEmployee.LastName = employee.LastName;
                foundEmployee.Zip = employee.Zip;
                foundEmployee.State = employee.State;
                foundEmployee.City = employee.City;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/ShowCustomersByDay
        public ActionResult ShowCustomersByDay(string day)
        {
            Employee employee;
            string signedInEmployeeApplicationId;
            IQueryable<Customer> customers;

            signedInEmployeeApplicationId = User.Identity.GetUserId();
            employee = _context.Employees.Where(e => e.ApplicationUserId.Equals(signedInEmployeeApplicationId)).Single();
            customers = _context.Customers.Where(c => c.Zip == employee.Zip && c.PickUpDay.Equals(day));

            return View(customers);
        }
    }
}
