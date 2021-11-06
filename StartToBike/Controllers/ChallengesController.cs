using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StartToBike.Models;
using StartToBike.ViewModels;

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
        public ActionResult Create([Bind(Include = "ChallengeId,ChallengeName,Reward,Task")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                Boolean Valid = challenge.CreateChallenge();

                if (Valid)
                {
                    Account logInAccount = db.Account.Find(Account.LogInAccount.AccountId);
                    Account friendToChallenge = db.Account.Find(Friend.FriendToChallenge.AccountId);

                    challenge.Account.Add(logInAccount);
                    challenge.Account.Add(friendToChallenge);

                    db.Challenge.Add(challenge);
                    db.SaveChanges();
                    return RedirectToAction("ChallengesAccount");
                }
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

        public ActionResult ChallengesAccount(string error)
        {
            ViewBag.ErrorMessage = error;

            ///<summary>
            ///Check if the user is already logged in 
            /// </summary>
            Account logInAccount = Account.LogInAccount;
            if (logInAccount == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            ///<summary>
            ///Fills in the challenges for the account who logged in
            /// </summary>
            var account = db.Account.Include(x => x.Challenge).FirstOrDefault(x => x.AccountId == logInAccount.AccountId);
            return View(account);
        }

        public ActionResult ChallengeCompleted(int id)
        {
            Challenge challenge = db.Challenge.Find(id);

            Boolean Valid = challenge.ChallengeCompleted();


            db.SaveChanges();

            if (Valid)
            {
                return RedirectToAction("ChallengesAccount", new { error = "You completed the challenge!" });
            }

            return RedirectToAction("ChallengesAccount", new { error = "Error, You can't complete the challenge!" });
        }
    }
}
