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
    public class QuestsController : Controller
    {
        readonly DBContext db = new DBContext();

        // GET: Quests
        public ActionResult Index()
        {
            return View(db.Quest.ToList());
        }

        // GET: Quests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quest.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // GET: Quests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestId,QuestLevel,QuestTask")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                Boolean Valid = quest.CreateQuest();
                if (Valid)
                {
                    Account logInAccount = Account.LogInAccount;


                    quest.Account.Add(logInAccount);


                    db.Quest.Add(quest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(quest);
        }

        // GET: Quests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quest.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestId,QuestLevel,QuestTask")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quest);
        }

        // GET: Quests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quest.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quest quest = db.Quest.Find(id);
            db.Quest.Remove(quest);
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

        public ActionResult QuestsAccount (string error)
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
            ///Fills in the quests for the account who logged in
            /// </summary>
            var account = db.Account.Include(x => x.Quest).FirstOrDefault(x => x.AccountId == logInAccount.AccountId);
            return View(account);
        }


        public ActionResult QuestCompleted(int id)
        {
            Quest quest = db.Quest.Find(id);

            Boolean Valid = quest.QuestCompleted();


            db.SaveChanges();

            if (Valid)
            {
                return RedirectToAction("QuestAccount", new { error = "You completed the quest!" });
            }

            return RedirectToAction("QuestAccount", new { error = "Error, You can't complete this quest!" });
        }
    }
}
