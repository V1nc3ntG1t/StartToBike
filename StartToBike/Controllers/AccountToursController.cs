using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StartToBike.Models;

namespace StartToBike.Controllers
{
    public class AccountToursController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AccountTours
        public ActionResult Index()
        {
            var accountTour = db.AccountTour.Include(a => a.Account).Include(a => a.Performance).Include(a => a.Tour);
            return View(accountTour.ToList());
        }

        // GET: AccountTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTour accountTour = db.AccountTour.Find(id);
            if (accountTour == null)
            {
                return HttpNotFound();
            }
            return View(accountTour);
        }

        // GET: AccountTours/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email");
            ViewBag.PerformanceId = new SelectList(db.Performance, "PerformanceId", "StartTime");
            ViewBag.PerformanceId = new SelectList(db.Tour, "TourId", "TourName");
            return View();
        }

        // POST: AccountTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,TourId,PerformanceId")] AccountTour accountTour)
        {
            if (ModelState.IsValid)
            {
                db.AccountTour.Add(accountTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTour.AccountId);
            ViewBag.PerformanceId = new SelectList(db.Performance, "PerformanceId", "StartTime", accountTour.PerformanceId);
            ViewBag.PerformanceId = new SelectList(db.Tour, "TourId", "TourName", accountTour.PerformanceId);
            return View(accountTour);
        }

        // GET: AccountTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTour accountTour = db.AccountTour.Find(id);
            if (accountTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTour.AccountId);
            ViewBag.PerformanceId = new SelectList(db.Performance, "PerformanceId", "StartTime", accountTour.PerformanceId);
            ViewBag.PerformanceId = new SelectList(db.Tour, "TourId", "TourName", accountTour.PerformanceId);
            return View(accountTour);
        }

        // POST: AccountTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,TourId,PerformanceId")] AccountTour accountTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTour.AccountId);
            ViewBag.PerformanceId = new SelectList(db.Performance, "PerformanceId", "StartTime", accountTour.PerformanceId);
            ViewBag.PerformanceId = new SelectList(db.Tour, "TourId", "TourName", accountTour.PerformanceId);
            return View(accountTour);
        }

        // GET: AccountTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTour accountTour = db.AccountTour.Find(id);
            if (accountTour == null)
            {
                return HttpNotFound();
            }
            return View(accountTour);
        }

        // POST: AccountTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountTour accountTour = db.AccountTour.Find(id);
            db.AccountTour.Remove(accountTour);
            db.SaveChanges();
            return RedirectToAction("Index");
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
