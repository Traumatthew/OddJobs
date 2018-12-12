using Microsoft.AspNet.Identity;
using OddJobs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddJobs.Controllers
{
    public class JobsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUser user = new ApplicationUser();

        // GET: Job For Logged in Cust
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());

            //var userId = User.Identity.GetUserId();
            //var LoggedInCust = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //var jobs = db.Jobs.Where(x => x.CustomerId == LoggedInCust.CustomerId).ToList();
            //return View(jobs);

            //var jobs = db.Customers.Include(j => j.Jobs).ToList();
            //return View(jobs);

            //Job job = new Job();
            //List<Job> Jobslist = db.Jobs.ToList();
            //return View(Jobslist);

            //var jobsList = db.Jobs.Where(j => j.JobId == j.JobId).ToList();
            //return View();
        }

        //GET: List Of All Jobs
        //public ActionResult Index2()
        //{
        //    return View(db.Jobs.ToList());
        //}

        [HttpGet]
        public ActionResult CreateJob()
        {
            var currentUser = User.Identity.GetUserId();
            Job job = new Job();
            return View(job);
            //string userId = User.Identity.GetUserId();
            //var loggedInContractor = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //var myJobs = db.Jobs.Where(j => j.ContractorId == loggedInContractor.ContractorId).Include(j => j.Customers).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJob([Bind(Include = "JobId,Location,Estimate,Details")] Customer customer, Job job)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var customerInDb = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
                job.CustomerId = customerInDb.CustomerId;
                SetCoords(job);
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);

        }

        public void SetCoords(Job job)
        {
            GeoLocation geo = new GeoLocation();
            var coords = geo.GetLatandLong(job);
            job.lat = coords["lat"];
            job.lng = coords["lng"];
            db.SaveChanges();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var job = db.Jobs.Where(x => x.JobId == x.JobId).FirstOrDefault();
            return View(job);
        }

        public ActionResult DeleteJob(int id)
        {
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            return View(job);
        }

        [HttpPost]
        public ActionResult DeleteJob(int id, FormCollection form)
        {
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Job job = db.Jobs.Find(id);
            return View(job);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Street,City,State,Zip,Date,Details,JobCategory")] Job job, int id)
        {
            if (ModelState.IsValid)
            {
                Job editedJob = db.Jobs.Find(id);

                if (editedJob == null)
                {
                    return RedirectToAction("DisplayError", "Contractors");
                }

                editedJob.Street = job.Street;
                editedJob.City = job.City;
                editedJob.State = job.State;
                editedJob.Zip = job.Zip;
                //editedJob.Location = job.Location;
                editedJob.Date = job.Date;
                editedJob.Details = job.Details;
                editedJob.JobCategory = job.JobCategory;

                db.Entry(editedJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(job);
        }
    }
}
