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
    public class TrainingAccountsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TrainingAccounts
        public ActionResult Index()
        {
            var trainingAccounts = db.TrainingAccounts.Include(t => t.ACCOUNT).Include(t => t.TRAINING);
            return View(trainingAccounts.ToList());
        }

        // GET: TrainingAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingAccount trainingAccount = db.TrainingAccounts.Find(id);
            if (trainingAccount == null)
            {
                return HttpNotFound();
            }
            return View(trainingAccount);
        }

        // GET: TrainingAccounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email");
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingID", "Title");
            return View();
        }

        // POST: TrainingAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingId,AccountId")] TrainingAccount trainingAccount)
        {
            if (ModelState.IsValid)
            {
                db.TrainingAccounts.Add(trainingAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", trainingAccount.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingID", "Title", trainingAccount.TrainingId);
            return View(trainingAccount);
        }

        // GET: TrainingAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingAccount trainingAccount = db.TrainingAccounts.Find(id);
            if (trainingAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", trainingAccount.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingID", "Title", trainingAccount.TrainingId);
            return View(trainingAccount);
        }

        // POST: TrainingAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingId,AccountId")] TrainingAccount trainingAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Account, "AccountId", "Email", trainingAccount.AccountId);
            ViewBag.TrainingId = new SelectList(db.Trainings, "TrainingID", "Title", trainingAccount.TrainingId);
            return View(trainingAccount);
        }

        // GET: TrainingAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingAccount trainingAccount = db.TrainingAccounts.Find(id);
            if (trainingAccount == null)
            {
                return HttpNotFound();
            }
            return View(trainingAccount);
        }

        // POST: TrainingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingAccount trainingAccount = db.TrainingAccounts.Find(id);
            db.TrainingAccounts.Remove(trainingAccount);
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
