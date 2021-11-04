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
    public class ToursController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Tours
        public ActionResult Index()
        {
            return View(db.Tour.ToList());
        }
        public ActionResult HomeScreen()
        {
            return View();
        }
        public ActionResult TourAccount()
        {
            return View();
        }
        
        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourId,TourName,Rating,Location,StartDate,Distance")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tour.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour);
        }

        // GET: Tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TourId,TourName,Rating,Location,StartDate,Distance")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tour.Find(id);
            db.Tour.Remove(tour);
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
        public  ActionResult JoinTour(int id)

        {
            // This is the log in account
            Account account = Account.LogInAccount;
            // this is the id of the tour
            Tour tOUR = db.Tour.Find(id);
            AccountTour logInAccount = db.AccountTour.First(g => g.TourId == id && g.AccountId == account.AccountId);

            var t = new AccountTour
            {
                TourId = tOUR.TourId,
                AccountId = logInAccount.AccountId,
            };

            Tour game = new Tour();
           

            var exists = db.AccountTour.Where(g => g.TourId == tOUR.TourId).Where(g => g.AccountId == logInAccount.AccountId).AnyAsync();
            // Hier moet ik kijken naar of het account de tour heeft gejoined
            if (exists)
            {
                return RedirectToAction("Index", new { error = "You already joined the game!" });
            }
            else
            {
                db.AccountTour.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index", new { error = "You succesfully joined the game!" });
            }

        }
        
    }
}
