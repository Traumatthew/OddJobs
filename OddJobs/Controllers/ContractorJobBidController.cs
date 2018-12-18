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
    public class ContractorJobBidController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUser user = new ApplicationUser();


        // GET: ContractorJobBid
        public ActionResult Index()
        {
            return View(db.ContractorJobBids.ToList());
        }

        // GET: ContractorJobBid/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var bid = db.ContractorJobBids.Where(x => x.BidId == id).FirstOrDefault();
            return View(bid);
        }

        // GET: ContractorJobBid/Create
        [HttpGet]
        public ActionResult CreateBid()
        {
            var jobBid = db.ContractorJobBids.ToList();
            ContractorJobBid contractorJobBid = new ContractorJobBid();
            return View(jobBid);
        }

        // POST: ContractorJobBid/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid([Bind(Include = "BidId,EstHoursToComplete,JobId,CustomerId,ContractorId")] ContractorJobBid contractorJobBid, int id)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                //var customerInDb = db.Customers.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
                //var tempCont = db.Contractors.Where(x => x.ContractorId == userId).SingleOrDefault();
                var contractorInDb = db.Contractors.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
                var jobInDb = db.Jobs.Where(j => j.JobId == id).SingleOrDefault();
                contractorJobBid.BidId = jobInDb.JobId;
                //job.CustomerId = customerInDb.CustomerId;
                db.Jobs.Add(jobInDb);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View("Index", "Index", "Jobs");
        }

        // GET: ContractorJobBid/Edit/5
        public ActionResult Edit(int id)
        {
            Job job = db.Jobs.Find(id);
            return View(job);
        }

        // POST: ContractorJobBid/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "BidId,EstHoursToComplete,MaterialCost,Date,BidAmt,JobId")] Job job, ContractorJobBid contractorJobBid, int id)
        {
            if (ModelState.IsValid)
            {
                ContractorJobBid editedContractorJobBid = db.ContractorJobBids.Find(id);
                if(editedContractorJobBid == null)
                {
                    return RedirectToAction("DisplayError", "Jobs");
                }

                editedContractorJobBid.EstHoursToComplete = contractorJobBid.EstHoursToComplete;
                editedContractorJobBid.MaterialCost = contractorJobBid.MaterialCost;
                editedContractorJobBid.Date = contractorJobBid.Date;
                editedContractorJobBid.BidAmt = contractorJobBid.BidAmt;
                db.Entry(contractorJobBid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(contractorJobBid);
        }

        // GET: ContractorJobBid/Delete/5
        public ActionResult Delete(int id)
        {
            var bidToDelete = db.ContractorJobBids.Where(x => x.BidId == id).FirstOrDefault();
            return View(bidToDelete);

            //ContractorJobBid contractorJobBid = db.ContractorJobBids.Find(id);
            //return View(contractorJobBid);
        }

        // POST: ContractorJobBid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            var bidToDelete = db.ContractorJobBids.Where(x => x.BidId == id).FirstOrDefault();
            db.ContractorJobBids.Remove(bidToDelete);
            db.SaveChanges();
            return RedirectToAction("Details", "Contractors");

            //ContractorJobBid contractorJobBid = db.ContractorJobBids.Find(id);
            //contractorJobBid.BidId = "Deleted";
            //db.Entry(contractorJobBid).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Details", "Jobs");


        }
    }
}
