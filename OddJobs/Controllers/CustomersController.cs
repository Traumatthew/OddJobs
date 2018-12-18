using Microsoft.AspNet.Identity;
using OddJobs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OddJobs.Controllers
{
    public class CustomersController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUser user = new ApplicationUser();


        // GET: Customers (Logged in Customer)
        [HttpGet]
        public ActionResult Index()
        {

            var userLoggedIn = db.Customers.Include(c => c.ApplicationUser);
            return View(userLoggedIn.ToListAsync());

            //var UserId = User.Identity.GetUserId();
            //var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).ToList();
            //return View(cust);
        }

        //Gets all customers **May need to move to contractor or job controller**
        [HttpGet]
        public ActionResult CustomerList()
        {
            var custList = db.Customers.ToList();
            return View(custList);
        }

        public ActionResult Create()
        {
            //var user = User.Identity.GetUserId();
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,Street,City,State,Zip,Phone,Email,lat,lng,CustomerWalletId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ApplicationUserId = User.Identity.GetUserId();
                SetCoords(customer);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(customer);
        }

        public void SetCoords(Customer cust)
        {
            GeoLocation geo = new GeoLocation();
            var coords = geo.GetLatandLong(cust);
            cust.lat = coords["lat"];
            cust.lng = coords["lng"];
            db.SaveChanges();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {

            var userId = User.Identity.GetUserId();
            var currentCust = db.Customers.Where(x => x.ApplicationUserId == userId).SingleOrDefault();
            return View(currentCust);

            //Customer customer = db.Customers.Find(id);
            //return View(customer);

            //var UserId = User.Identity.GetUserId();
            //var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).ToList();
            //return View(cust);

        }

        [HttpGet]
        public ActionResult Customer_ContractorDetails(int? id)
        {
            var contInDb = db.Contractors.Where(x => x.ContractorId == id).FirstOrDefault();
            return View(contInDb);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Street,City,State,Zip,Phone,Email")] Customer customer, int id)
        {
            if (ModelState.IsValid)
            {
                Customer editedCustomer = db.Customers.Find(id);

                if (editedCustomer == null)
                {
                    return RedirectToAction("DisplayError", "Customers");
                }

                editedCustomer.FirstName = customer.FirstName;
                editedCustomer.LastName = customer.LastName;
                editedCustomer.Street = customer.Street;
                editedCustomer.City = customer.City;
                editedCustomer.State = customer.State;
                editedCustomer.Zip = customer.Zip;
                editedCustomer.Phone = customer.Phone;
                editedCustomer.Email = customer.Email;
                db.Entry(editedCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            Customer customer = db.Customers.Find(id);
            customer.FirstName = "Deleted";
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("LogOff", "Account");
        }

        public ActionResult ListOfContractors()
        {
            return View(db.Contractors.ToList());
            //var contList = db.Customers.Include(c => c.Contractors).ToList();
            //return View(contList);
        }

        [HttpGet]
        public ActionResult ViewMyJobRequests()
        {
            var userId = User.Identity.GetUserId();
            var LoggedInCust = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            var jobs = db.Jobs.Where(x => x.CustomerId == LoggedInCust.CustomerId).ToList();
            //var custJobs = db.Jobs.Where(j => j.CustomerId == LoggedInCust.CustomerId && j => j.Jobs == j.JobId).FirstOrDefault(); //.Include(j => j.JobId).ToList();
            return View(jobs);


            //var currentCust = db.Customers.Where(x => x.ApplicationUserId == userId).SingleOrDefault();
            //var UserJobs = db.Customers.w

            //string userId = User.Identity.GetUserId();
            //var loggedInContractor = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //var myJobs = db.Jobs.Where(j => j.ContractorId == loggedInContractor.ContractorId).Include(j => j.Customers).ToList();

        }
    }
}
