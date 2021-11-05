using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StartToBike.Models;
using StartToBike.ViewModels;

namespace StartToBike.Controllers
{
    public class TrainingsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Trainings
        public ActionResult Index()
        {
            return View(db.Trainings.ToList());
        }

        // GET: Trainings/Catalog
        public ActionResult Catalog()
        {
            return View(db.Trainings.ToList());
        }

        // POST: Trainings/Join/5
        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            else
                return View(training);
        }

        // POST: Game/Join/5
        [HttpPost, ActionName("Join")]
        [ValidateAntiForgeryToken]
        public async ActionResult JoinConfirmed(int id)
        {
            Training training = db.Trainings.Find(id);
            Account logInAccount = Account.LogInAccount;

            var t = new TrainingAccount
            {
                TrainingId = Training.TrainingID,
                AccountId = logInAccount.AccountId,
            };

            Training training1 = new Training();

            var exists = await db.TrainingAccounts.Where(g => g.TrainingId == training1.TrainingID).Where(g => g.AccountId == logInAccount.AccountId).Any();
            if (exists)
            {
                return RedirectToAction("TrainingAccount", new { error = "You already joined the Training!" });
            }
            else
            {
                db.TrainingAccounts.Add(t);
                db.SaveChanges();
                return RedirectToAction("TrainingAccount", new { error = "You succesfully joined the Training!" });
            }
        }

        // GET: Trainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usersPerTraining = new UsersPerTraining();
            usersPerTraining.Training = db.Trainings.Where(g => g.TrainingID == id).Include(g => g.Users).FirstOrDefault();
            usersPerTraining.Users = usersPerTraining.Training.Users;

            if (usersPerTraining.Training == null)
            {
                return HttpNotFound();
            }
            return View(usersPerTraining);

           /* Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);*/
        }

        // GET: Trainings/RemoveUserFromTraining?gameid=1&userid=1
        public ActionResult RemoveUserFromTraining(int? trainingid, int? accountid)
        {
            if (trainingid == null || accountid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userPerTraining = new UsersPerTraining();
            var theTraining = db.Trainings.Where(g => g.TrainingID == trainingid).Include(g => g.Users).FirstOrDefault();
            Account userToRemove = theTraining.Users.Single(u => u.AccountId == accountid);
            theTraining.Users.Remove(userToRemove);
            db.SaveChanges();

            userPerTraining.Training = theTraining;
            userPerTraining.Users = userPerTraining.Training.Users;

            if (userPerTraining.Training == null)
            {
                return HttpNotFound();
            }
            return View("Details", userPerTraining);
        }

        // GET: Game/RemoveUserFromTraining?gameid=1&userid=1
        public ActionResult AddAllUsersToTraining(int? trainingid)
        {
            if (trainingid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userPerTraining = new UsersPerTraining();
            var theTraining = db.Trainings.Where(g => g.TrainingID == trainingid).Include(g => g.Users).FirstOrDefault();

            // add all users
            var allUsers = db.Account.ToList<Account>();
            if (theTraining.AddUsersToTraining(allUsers))
            {
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userPerTraining.Training = theTraining;
            userPerTraining.Users = userPerTraining.Training.Users;

            if (userPerTraining.Training == null)
            {
                return HttpNotFound();
            }
            return View("Details", userPerTraining);
        }

        // GET: Trainings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingID,Title,Level")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Trainings.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(training);
        }

        // GET: Trainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingID,Title,Level")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

        // GET: Trainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Trainings.Find(id);
            db.Trainings.Remove(training);
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
