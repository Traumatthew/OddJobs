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

        // GET: Jobs
        public ActionResult Index()
        {
            Job job = new Job();
            //List<Job> Jobslist = db.Jobs.ToList();

            var jobsList = db.Jobs.Where(j => j.JobId == j.JobId).ToList(); 
            return View();
        }

        [HttpGet]
        public ActionResult CreateJob()
        {
            return View();
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

                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var job = db.Jobs.Where(x => x.JobId == x.JobId).SingleOrDefault();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Location,Date,Details,JobCategory")] Job job, int id)
        {
            if (ModelState.IsValid)
            {
                Job editedJob = db.Jobs.Find(id);

                if (editedJob == null)
                {
                    return RedirectToAction("DisplayError", "Contractors");
                }

                editedJob.Location = job.Location;
                editedJob.Date = job.Date;
                editedJob.Details = job.Details;
                editedJob.JobCategory = job.JobCategory;

                db.Entry(editedJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }
    }
}
