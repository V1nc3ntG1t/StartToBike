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

        // GET: Trainings/Admin
        public ActionResult Admin()
        {
            return View(db.Trainings.ToList());
        }

        // GET: Trainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usersPerTraining = new UsersPerTraining();
            usersPerTraining.Training = db.Trainings.Where(g => g.TrainingId == id).Include(g => g.Users).FirstOrDefault();
            usersPerTraining.Users = usersPerTraining.Training.Users;

            if (usersPerTraining.Training == null)
            {
                return HttpNotFound();
            }
            return View(usersPerTraining);
        }

        // GET: Trainings/RemoveUserFromTraining?gameid=1&userid=1
        public ActionResult RemoveUserFromTraining(int? trainingid, int? accountid)
        {
            if (trainingid == null || accountid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userPerTraining = new UsersPerTraining();
            var theTraining = db.Trainings.Where(g => g.TrainingId == trainingid).Include(g => g.Users).FirstOrDefault();
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
            var theTraining = db.Trainings.Where(g => g.TrainingId == trainingid).Include(g => g.Users).FirstOrDefault();

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
        public ActionResult Create([Bind(Include = "TrainingId,Title,TrainingLevel")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Trainings.Add(training);
                db.SaveChanges();
                return RedirectToAction("Admin");
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
        public ActionResult Edit([Bind(Include = "TrainingId,Title,TrainingLevel")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
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
            return RedirectToAction("Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

        // POST: Trainings/JoinTraining/5
        public ActionResult JoinTraining(int id)

        {
            Account account = Account.LogInAccount;
            Training training = db.Trainings.Find(id);


            var t = new AccountTraining
            {
                TrainingId = training.TrainingId,
                AccountId = account.AccountId,
            };

            Training game = new Training();

            var exists = db.AccountTrainings.Where(g => g.TrainingId == game.TrainingId).Where(g => g.AccountId == account.AccountId).Any();


            if (exists)
            {
                return RedirectToAction("Catalog", new { error = "You already joined this Training!" });
            }
            else
            {
                db.AccountTrainings.Add(t);
                db.SaveChanges();
                return RedirectToAction("Catalog", new { error = "You succesfully joined the Training!" });
            }

        }
        public ActionResult JoinLandPage(int? id)
        {
            Training training = db.Trainings.Find(id);
            Account logInAccount = Account.LogInAccount;

            UsersPerTraining.TrainingLoaded = db.Trainings.Find(id);

            return View();
        }
    }
}
