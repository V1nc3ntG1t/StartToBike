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
    public class AccountCatalogsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AccountCatalogs
        public ActionResult Index()
        {
            return View(db.AccountCatalog.ToList());
        }

        // GET: AccountCatalogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountCatalog accountCatalog = db.AccountCatalog.Find(id);
            if (accountCatalog == null)
            {
                return HttpNotFound();
            }
            return View(accountCatalog);
        }

        // GET: AccountCatalogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,AccountRole")] AccountCatalog accountCatalog)
        {
            if (ModelState.IsValid)
            {
                db.AccountCatalog.Add(accountCatalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountCatalog);
        }

        // GET: AccountCatalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountCatalog accountCatalog = db.AccountCatalog.Find(id);
            if (accountCatalog == null)
            {
                return HttpNotFound();
            }
            return View(accountCatalog);
        }

        // POST: AccountCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,AccountRole")] AccountCatalog accountCatalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountCatalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountCatalog);
        }

        // GET: AccountCatalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountCatalog accountCatalog = db.AccountCatalog.Find(id);
            if (accountCatalog == null)
            {
                return HttpNotFound();
            }
            return View(accountCatalog);
        }

        // POST: AccountCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountCatalog accountCatalog = db.AccountCatalog.Find(id);
            db.AccountCatalog.Remove(accountCatalog);
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
