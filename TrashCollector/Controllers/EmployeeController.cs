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
        public EmployeeController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult Index()
        {
            string signedInEmployeeApplicationId = User.Identity.GetUserId();
            Employee signedInEmployee = _context.Employees.Find(signedInEmployeeApplicationId);

            /*IQueryable<Customer> customers = _context.Where(c => c.sig );*/
            return View();
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

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
