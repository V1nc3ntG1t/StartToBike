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
    public class AccountTrainingsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AccountTrainings
        public ActionResult Index()
        {
            var accountTrainings = db.AccountTrainings.Include(a => a.Account).Include(a => a.Training);
            return View(accountTrainings.ToList());
        }

        // GET: AccountTrainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTraining accountTraining = db.AccountTrainings.Find(id);
            if (accountTraining == null)
            {
                return HttpNotFound();
            }
            return View(accountTraining);
        }

        // GET: AccountTrainings/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email");
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingId", "Title");
            return View();
        }

        // POST: AccountTrainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,TrainingId")] AccountTraining accountTraining)
        {
            if (ModelState.IsValid)
            {
                db.AccountTrainings.Add(accountTraining);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTraining.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingId", "Title", accountTraining.TrainingId);
            return View(accountTraining);
        }

        // GET: AccountTrainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTraining accountTraining = db.AccountTrainings.Find(id);
            if (accountTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTraining.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingId", "Title", accountTraining.TrainingId);
            return View(accountTraining);
        }

        // POST: AccountTrainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,TrainingId")] AccountTraining accountTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", accountTraining.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingId", "Title", accountTraining.TrainingId);
            return View(accountTraining);
        }

        // GET: AccountTrainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountTraining accountTraining = db.AccountTrainings.Find(id);
            if (accountTraining == null)
            {
                return HttpNotFound();
            }
            return View(accountTraining);
        }

        // POST: AccountTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountTraining accountTraining = db.AccountTrainings.Find(id);
            db.AccountTrainings.Remove(accountTraining);
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
