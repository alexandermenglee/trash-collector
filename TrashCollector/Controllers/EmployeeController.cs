﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            DayOfWeek today = DateTime.Today.DayOfWeek;

            return RedirectToAction("IndexByDay", "Employee", new { day = today });
        }

        // Employee/Index/{day}
        public ActionResult IndexByDay(string day)
        {
            Employee employee;
            string signedInEmployeeApplicationId;
            IQueryable<Customer> customers;
            ViewBag.days = days;

            signedInEmployeeApplicationId = User.Identity.GetUserId();
            employee = _context.Employees.Where(e => e.ApplicationUserId.Equals(signedInEmployeeApplicationId)).Single();
            customers = _context.Customers.Where(c => c.Zip == employee.Zip && c.PickUpDay.Equals(day) && c.AccountSuspended == false);

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

        public ActionResult ConfirmPickup(Customer customer)
        {
            Customer foundCustomer;
            
            foundCustomer = _context.Customers.Find(customer.CustomerId);
            foundCustomer.PickedUp = true;
            foundCustomer.BillAmount += 2.75;

            _context.SaveChanges();

            return RedirectToAction("IndexByDay", "Employee" , new { day = customer.PickUpDay });
        }

        [HttpGet]
        public ActionResult ShowCustomerOnMap(int? id)
        {
            Customer customer = _context.Customers.Find(id);
            ViewBag.address = $"{customer.Street} {customer.City} {customer.Zip}";

            return View(customer);
        }
    }
}
