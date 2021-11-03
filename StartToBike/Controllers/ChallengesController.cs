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
    public class ChallengesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Challenges
        public ActionResult Index()
        {
            return View(db.Challenge.ToList());
        }

        // GET: Challenges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenge.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // GET: Challenges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Challenges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChallengeId,ChallengeName,StartDate,Reward,Task")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                db.Challenge.Add(challenge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(challenge);
        }

        // GET: Challenges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenge.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // POST: Challenges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChallengeId,ChallengeName,StartDate,Reward,Task")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(challenge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(challenge);
        }

        // GET: Challenges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenge.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // POST: Challenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Challenge challenge = db.Challenge.Find(id);
            db.Challenge.Remove(challenge);
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
