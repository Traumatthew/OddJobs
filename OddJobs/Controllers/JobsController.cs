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

        public ActionResult Index2()
        {
            return View(db.Jobs.ToList());
        }


        //GET
        [HttpGet]
        public ActionResult CreateJob(int? id)
        {
            //Job job = db.Jobs.Find(id);
            var jobCat = db.JobCategories.ToList();
            Job job = new Job()
            {
                JobCategories = jobCat
            };
            return View(job);


            //var currentUser = User.Identity.GetUserId();

            //Drop down for picking the  category of customers job
            //var jobCat = db.JobCategories.ToList();
            //Job job = new Job()
            //{
            //    JobCategories = jobCat
            //};

            //return View(job);

            //string userId = User.Identity.GetUserId();
            //var loggedInContractor = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //var myJobs = db.Jobs.Where(j => j.ContractorId == loggedInContractor.ContractorId).Include(j => j.Customers).ToList();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult CreateJob(Job job)
        public ActionResult CreateJob([Bind(Include = "JobId,Street,City,State,Zip,Date,Details,lat,lng,JobCategory")] Job job)
        {

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var currentCust = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
            //    job.CustomerId = currentCust.CustomerId;
            //    SetCoords(job);
            //    db.Jobs.Add(job);
            //    db.SaveChanges();
            //    return RedirectToAction("Details");
            //}
            //return View(job);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var currentCust = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
                job.JobId = job.JobId;
                job.CustomerId = currentCust.CustomerId;
                SetCoords(job);
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("JobDetails");
            }
            return View(job);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //var userId = User.Identity.GetUserId();
            //var currentCust = (from c in db.Customers where userId == c.ApplicationUserId select c).First();
            //currentCust.CustomerId = job.JobId;
            //db.Entry(currentCust).State = EntityState.Modified;
            //SetCoords(job);
            //db.Jobs.Add(job);
            //db.SaveChanges();
            //return RedirectToAction("Details");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            //    customer.ApplicationUserId = User.Identity.GetUserId();


            //    var currentJob = db.Jobs.Where(j => j.JobId == customer.CustomerId);
            //    //job.CatId = job.CatId;
            //    SetCoords(job);
            //    db.Jobs.Add(job);
            //    db.SaveChanges();
            //    return RedirectToAction("Details");
            //}
            //return View(job);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    customer.ApplicationUserId = User.Identity.GetUserId();
            //    SetCoords(customer);
            //    db.Customers.Add(customer);
            //    db.SaveChanges();
            //    return RedirectToAction("Details");
            //}
            //return View(customer);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //var userId = User.Identity.GetUserId();
            //var customerInDb = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //job.CustomerId = customerInDb.CustomerId;
            //var jobInDb = db.Jobs.Where(j => j.JobId == job.JobId).FirstOrDefault();

            //if (ModelState.IsValid)
            //{
            //    var JobInDb = db.Jobs.Single(j => j.JobId == job.JobId);
            //    JobInDb.Street = job.Street;
            //    JobInDb.City = job.City;
            //    JobInDb.State = job.State;
            //    JobInDb.Zip = job.Zip;
            //    JobInDb.Estimate = job.Estimate;
            //    JobInDb.Details = job.Details;
            //    JobInDb.CatId = job.CatId;
            //    JobInDb.CustomerId = job.CustomerId;
            //    JobInDb.ContractorId = job.ContractorId;
            //}

            //SetCoords(job);
            //db.SaveChanges();
            //return RedirectToAction("Details");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (job.JobId == 0)
            //{
            //    db.Jobs.Add(job);
            //}
            //else
            //{
            //    var JobInDb = db.Jobs.Single(j => j.JobId == job.JobId);
            //    //var JobInDb = db.Jobs.Where(j => j.JobId == job.JobId).SingleOrDefault();
            //    JobInDb.Street = job.Street;
            //    JobInDb.City = job.City;
            //    JobInDb.State = job.State;
            //    JobInDb.Zip = job.Zip;
            //    JobInDb.Estimate = job.Estimate;
            //    JobInDb.Details = job.Details;
            //    JobInDb.CatId = job.CatId;
            //    JobInDb.CustomerId = job.CustomerId;
            //    JobInDb.ContractorId = job.ContractorId;
            //}
            //SetCoords(job);
            ////db.Jobs.Add(job);
            //db.SaveChanges();
            //return RedirectToAction("Details");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var customerInDb = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
            //    job.CustomerId = customerInDb.CustomerId;
            //    SetCoords(job);
            //    db.Jobs.Add(job);
            //    db.SaveChanges();
            //    return RedirectToAction("Details");
            //}

            //return View("Details");

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
        public ActionResult JobDetails(int? id)
        {
            var userId = User.Identity.GetUserId();
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            return View(job);
        }

        [HttpGet]
        public ActionResult ViewCustomerJobDetails(int? id)
        {
            var userId = User.Identity.GetUserId();
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            return View(job);
        }

        [HttpGet]
        public ActionResult DeleteJob(int? id)
        {
            var userId = User.Identity.GetUserId();
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            return View(job);
        }

        [HttpPost]
        public ActionResult DeleteJob(int? id, FormCollection form)
        {
            var job = db.Jobs.Where(x => x.JobId == id).FirstOrDefault();
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("ViewMyJobRequests", "Customers");
        }

        [HttpGet]
        public ActionResult EditJob(int? id)
        {
            var job = db.Jobs.SingleOrDefault(j => j.JobId == id);
            job.JobCategories = db.JobCategories.ToList();
            if(job == null)
            {
                return HttpNotFound();
            }
            return View(job);
            //Job job = db.Jobs.Find(id);
            //return View(job);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJob([Bind(Include = "JobId,Street,City,State,Zip,Date,Details,JobCategory")] Job job, int? id)
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
                editedJob.Date = job.Date;
                editedJob.Details = job.Details;
                editedJob.JobCategory = job.JobCategory;

                db.Entry(editedJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View("ViewMyJobRequests", "Customers");
        }
    }
}
