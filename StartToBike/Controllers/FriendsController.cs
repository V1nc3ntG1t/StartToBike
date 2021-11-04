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
    public class FriendsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Friends
        public ActionResult Index()
        {
            var friend = db.Friend.Include(f => f.Account).Include(f => f.Account1);
            return View(friend.ToList());
        }

        // GET: Friends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // GET: Friends/Create
        public ActionResult Create()
        {
            ViewBag.Friend1Id = new SelectList(db.Account, "AccountId", "UserName");
            ViewBag.Friend2Id = new SelectList(db.Account, "AccountId", "UserName");
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Friend1Id,Friend2Id,StartDate")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Friend.Add(friend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Friend1Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend1Id);
            ViewBag.Friend2Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend2Id);
            return View(friend);
        }

        // GET: Friends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            ViewBag.Friend1Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend1Id);
            ViewBag.Friend2Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend2Id);
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Friend1Id,Friend2Id,StartDate")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Friend1Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend1Id);
            ViewBag.Friend2Id = new SelectList(db.Account, "AccountId", "UserName", friend.Friend2Id);
            return View(friend);
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Friend friend = db.Friend.Find(id);
            db.Friend.Remove(friend);
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

        public ActionResult FriendsAccount()
        {
            ///<summary>
            ///Check if the user is already logged in 
            /// </summary>
            Account logInAccount = Account.LogInAccount;
            if (logInAccount == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            AccountFriends accountFriends = new AccountFriends();

            accountFriends.Account = db.Account.Find(logInAccount.AccountId);
            accountFriends.Friends = db.Friend.Where(a => a.Friend1Id == logInAccount.AccountId).Select(a => a.Account);
            accountFriends.Friends = db.Friend.Where(a => a.Friend2Id == logInAccount.AccountId).Select(a => a.Account);



            return View(accountFriends);
        }
    }
}
