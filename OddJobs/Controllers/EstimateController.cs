using Microsoft.AspNet.Identity;
using OddJobs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddJobs.Controllers
{
    public class EstimateController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUser user = new ApplicationUser();


        // GET: ContractorJobBid
        public ActionResult Index()
        {
            return View(db.Estimates.ToList());
        }

        //public Job GetJob()
        //{
        //    var job = db.Jobs.Where(j => j.JobId == j.JobId).FirstOrDefault();
        //    return job;
        //}

        // GET: ContractorJobBid/Details/5
        [HttpGet]
        public ActionResult EstDetails(int? id)
        {
            var userId = User.Identity.GetUserId();
            var est = db.Estimates.Where(x => x.EstId == id).FirstOrDefault();
            return View(est);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //[HttpGet]
        //public ActionResult CreateEst(int? id)
        //{
        //    var user = User.Identity.GetUserId();
        //    var jobToBeEst = db.Jobs.Where(j => j.JobId == id).SingleOrDefault();
        //    Estimate estimate = new Estimate();
        //    return View("Job_CreateEst");
        //}

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateEst([Bind(Include = "EstId,JobId,ContractorId,Date,BidAmt,EstHoursToComplete,MaterialCost")] Estimate estimate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = User.Identity.GetUserId();
        //        var currentCont = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
        //        var tempBid = db.Estimates.Where(x => x.EstId == estimate.EstId).FirstOrDefault();
        //        estimate.ContractorId = currentCont.ContractorId;
        //        estimate.EstId = tempBid.EstId;
        //        db.Estimates.Add(estimate);
        //        db.SaveChanges();
        //        return RedirectToAction("EstDetails");

        //    }
        //    return View("EstDetails");

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////}

        //GET: ContractorJobBid/Create
        //[HttpGet]
        // public ActionResult CreateEst(int? id)
        // {

        //     //var userId = User.Identity.GetUserId();
        //     //var job = db.Jobs.Where(j => j.JobId == JobId).FirstOrDefault();

        //     //var jobEst = db.Estimates.ToList();
        //     Estimate estimate = new Estimate();
        //     var jobInDb = id;
        //     //var jobInDb = db.Jobs.Where(j => j.JobId == id).FirstOrDefault();

        //     return View("Job_CreateEst");
        // }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public ActionResult CreateEst(int? id)
        {
            //var userId = User.Identity.GetUserId();
            var jobInDb = db.Jobs.SingleOrDefault(x => x.JobId == id);
            //var jobInDd = db.Jobs.Where(j => j.JobId == id).FirstOrDefault();
            Job_EstimateVM jobEstimate = new Job_EstimateVM()
            {
                estimate = new Estimate(),
                job = db.Jobs.Where(j => j.JobId == id).Single()
                
            };

            return View("Job_CreateEst", jobEstimate);
            //return View(Est);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: ContractorJobBid/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Job_CreateEst(Estimate estimate, Job job, int? id)
        public ActionResult CreateEst([Bind(Include = "EstId,JobId,EstHoursToComplete,MaterialCost,Date,BidAmt,CustomerId,ContractorId")] Estimate estimate, int? id, Job_EstimateVM jobEstimate)
        {
            //try
            //{
            //    if( ModelState.IsValid)
            //    {
            //        db.Estimates.Add(estimate);
            //        db.SaveChanges();
            //        return RedirectToAction("EstDetails");
            //    }
            //}
            //catch(DataException /* dex*/)
            //{
            //    //Log the error (uncomment dex variable name and add a line here to write a log.
            //    ModelState.AddModelError("", "Unable to save changes. Try again.");
            //}
            //return View(estimate);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //    TempData["Bid"] = contractorJobBid;
            //    return RedirectToAction("BidDetails");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (id == 0)
            {
                db.Estimates.Add(estimate);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var currentCont = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault(); //Gets logged in contractor
                var jobInDb = db.Jobs.Where(x => x.JobId == id).FirstOrDefault(); //Gets the Job Id
                var estInDb = db.Estimates.Where(e => e.EstId == estimate.EstId).FirstOrDefault();
                //Estimate estInDb = db.Estimates.Single(b => b.EstId == estimate.EstId);

                //var bidInDb = db.ContractorJobBids.Where(b => b.BidId == contractorJobBid.JobId).Single();
                estInDb.EstHoursToComplete = estimate.EstHoursToComplete;
                estInDb.MaterialCost = estimate.MaterialCost;
                estInDb.Date = estimate.Date;
                estInDb.BidAmt = estimate.BidAmt;
                db.Estimates.Add(estimate);
            }
            db.SaveChanges();
            return RedirectToAction("EstDetails");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var currentCont = db.Contractors.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
            //    estimate.EstId = estimate.EstId;
            //    estimate.ContractorId = currentCont.ContractorId;
            //    //SetCoords(estimate);
            //    db.Estimates.Add(estimate);
            //    db.SaveChanges();
            //    return RedirectToAction("EstDetails");
            //}
            //return View(estimate);


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var currentCont = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault(); //Gets logged in contractor
            //    var jobInDb = db.Jobs.Where(x => x.JobId == id).FirstOrDefault(); //Gets the Job Id 
            //    jobInDb.ContractorId = currentCont.ContractorId;
            //    jobInDb.JobId = jobInDb.JobId;
            //    db.Estimates.Add(estimate);
            //    db.SaveChanges();
            //    return RedirectToAction("EstDetails");

            //}
            //return View("EstIndex");

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if (ModelState.IsValid)
            //{
            //    var userId = User.Identity.GetUserId();
            ////var customerInDb = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
            ////var tempCont = db.Contractors.Where(x => x.ContractorId == userId).SingleOrDefault();
            //var contractorInDb = db.Contractors.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
            //var jobInDb = db.Jobs.Where(j => j.JobId == id).SingleOrDefault();

            //contractorJobBid.BidId = jobInDb.JobId;
            ////job.CustomerId = customerInDb.CustomerId;
            //db.Jobs.Add(jobInDb);
            //db.SaveChanges();
            //return RedirectToAction("Details");
            //}

            //return View("Index", "Index", "Jobs");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: ContractorJobBid/Edit/5
        public ActionResult EditEst(int id)
        {
            Job job = db.Jobs.Find(id);
            return View(job);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: ContractorJobBid/Edit/5
        [HttpPost]
        public ActionResult EditEst([Bind(Include = "EstId,EstHoursToComplete,MaterialCost,Date,BidAmt,JobId")] Job job, Estimate estimate, int id)
        {
            if (ModelState.IsValid)
            {
                Estimate editedEstimate = db.Estimates.Find(id);
                if(editedEstimate == null)
                {
                    return RedirectToAction("DisplayError", "Jobs");
                }

                editedEstimate.EstHoursToComplete = estimate.EstHoursToComplete;
                editedEstimate.MaterialCost = estimate.MaterialCost;
                editedEstimate.Date = estimate.Date;
                editedEstimate.BidAmt = estimate.BidAmt;
                db.Entry(estimate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(estimate);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: ContractorJobBid/Delete/5
        public ActionResult DeleteEst(int id)
        {
            var bidToDelete = db.Estimates.Where(x => x.EstId == id).FirstOrDefault();
            return View(bidToDelete);

            //ContractorJobBid contractorJobBid = db.ContractorJobBids.Find(id);
            //return View(contractorJobBid);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: ContractorJobBid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEstConfirmation(int id)
        {
            var estToDelete = db.Estimates.Where(x => x.EstId == id).FirstOrDefault();
            db.Estimates.Remove(estToDelete);
            db.SaveChanges();
            return RedirectToAction("Details", "Contractors");

            //ContractorJobBid contractorJobBid = db.ContractorJobBids.Find(id);
            //contractorJobBid.BidId = "Deleted";
            //db.Entry(contractorJobBid).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Details", "Jobs");


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //public void SetCoords(Estimate est)
        //{
        //    GeoLocation geo = new GeoLocation();
        //    var coords = geo.GetLatandLong(est);
        //    est.lat = coords["lat"];
        //    est.lng = coords["lng"];
        //    db.SaveChanges();
        //}
    }
}
