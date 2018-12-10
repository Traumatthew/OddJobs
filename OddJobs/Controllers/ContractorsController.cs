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
    public class ContractorsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUser user = new ApplicationUser();

        // GET: Contractors
        [HttpGet]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var cont = db.Contractors.Where(c => c.ApplicationUserId == UserId).ToList();
            return View(cont);

            //string userId = User.Identity.GetUserId();
            //var loggedInContractor = db.Contractors.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //var myJobs = db.Jobs.Where(j => j.ContractorId == loggedInContractor.ContractorId).Include(j => j.Customers).ToList();
            //return View(myJobs);
        }

        // GET: Contractors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractorId,ContractorName,ContractorStreet,ContractorCity,ContractorState,ContractorZip,ContractorPhone,ContractorEmail,AreaOfExpertise")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                contractor.ApplicationUserId = User.Identity.GetUserId();
                SetCoords(contractor);
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractor);
        }

        public void SetCoords(Contractor cont)
        {
            GeoLocation geo = new GeoLocation();
            var coords = geo.GetLatandLong(cont);
            cont.lat = coords["lat"];
            cont.lng = coords["lng"];
            db.SaveChanges();
        }

        // GET: Contractors/Details/5
        public ActionResult Details(int? id)
        {

            var userId = User.Identity.GetUserId();
            var currentCont = db.Contractors.Where(x => x.ApplicationUserId == userId).SingleOrDefault();
            return View(currentCont);

            //Contractor contractor = db.Contractors.Find(id);
            // return View(contractor);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Contractor contractor = db.Contractors.Find(id);

            //if (contractor == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contractor contractor = db.Contractors.Find(id);

            if (contractor == null)
            {
                return HttpNotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractorId,ContractorName,ContractorStreet,ContractorCity,ContractorState,ContractorZip,ContractorPhone,ContractorEmail,AreaOfExpertise")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractor);
        }


        //GET: Contractor/EditRating
        public ActionResult EditRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contractor contractor = db.Contractors.Find(id);
            var oldRate = contractor.Rating;
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        //POST: Contractor/EditRating
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRating([Bind(Include = "ContractorId,ContratorName,ContractorEmail,Rating,RatingData,TempRating,ApplicationUserId")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                contractor.RatingData += contractor.TempRating;
                var rateCharArray = contractor.RatingData.ToCharArray();
                int[] rateIntArray = Array.ConvertAll(rateCharArray, c => (int)Char.GetNumericValue(c));
                double rateAvg = rateIntArray.Average();
                contractor.Rating = rateAvg;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractor);
        }

        //GET: Delete/Contractor
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

         //POST: Delete/Contractor
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetListOfJobs()
        {
            var custList = db.Contractors.Include(c => c.Customers).ToList();
            return View(custList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
