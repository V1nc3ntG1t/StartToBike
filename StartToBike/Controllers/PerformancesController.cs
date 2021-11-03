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
    public class PerformancesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Performances
        public ActionResult Index()
        {
            return View(db.Performance.ToList());
        }

        // GET: Performances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerformanceId,Distance,Altitude,StartTime,EndTime")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performance.Add(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(performance);
        }

        // GET: Performances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerformanceId,Distance,Altitude,StartTime,EndTime")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performance);
        }

        // GET: Performances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.Performance.Find(id);
            db.Performance.Remove(performance);
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
