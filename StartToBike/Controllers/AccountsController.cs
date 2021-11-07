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
    public class AccountsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Accounts
        public ActionResult Index()
        {
            var account = db.Account.Include(a => a.AccountCatalog);
            return View(account.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

      
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create(string error)
        {
            ///<summary>
            ///Shows the user the specific error message
            /// </summary>
            ViewBag.ErrorMessage = error;

            ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AccountId,Email,Name,UserName,BirthDate,Gender,Password,City,RoleId,TrainingLevelString")] Account _account, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var exists = await db.Account.Where(a => a.UserName == _account.UserName).AnyAsync();
                if (exists)
                {
                    return RedirectToAction("Create", new { error = "This UserName already exists!" });
                }
                else
                {
                    bool Valid = _account.CreateAccount();
                    if (Valid)
                    {
                        ///<summary>
                        ///Add picture
                        /// </summary>
                        if (image != null)
                        {
                            _account.Picture = new byte[image.ContentLength];
                            image.InputStream.Read(_account.Picture, 0, image.ContentLength);
                        }

                        db.Account.Add(_account);

                        db.SaveChanges();
                        return RedirectToAction("Login", "Accounts");
                    }

                    ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole", _account.RoleId);
                    return View(_account);
                }
            }

            ///<summary>
            ///The User will see an error message if something is not right
            /// </summary>
            ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole", _account.RoleId);
            return View(_account);


            //     if (ModelState.IsValid)
            //     {
            //         db.Account.Add(account);
            //         db.SaveChanges();
            //         return RedirectToAction("Index");
            //     }
            //
            //     ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole", account.RoleId);
            //     return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole", account.RoleId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Email,Name,BirthDate,Gender,Password,City,Picture,RoleId,TrainingId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AccountCatalog, "RoleId", "AccountRole", account.RoleId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.Find(id);

            ///<summary>
            ///removes the account
            /// </summary>
            db.Account.Remove(account);
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

        public ActionResult Login(string error)
        {
            ViewBag.ErrorMessage = error;
            return View();

        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "UserName,Password")] Account _account)
        {
            var exists = await db.Account.Where(a => a.UserName == _account.UserName).Where(a => a.Password == _account.Password).AnyAsync();
            if (exists)
            {
                Account logInAccount = db.Account.First(a => a.UserName == _account.UserName);

                Account.LogInAccount = logInAccount;

                return RedirectToAction("", "");
            }

            else
            {
                // foutmelding
                return RedirectToAction("Login", new { error = "Try again!" });
            }
        }

        // GET: Accounts/Details/5
        public ActionResult DetailsLogInAccount()
        {
            ///<summary>
            ///Gets the account who logged in
            /// </summary>
            Account logInAccount = Account.LogInAccount;
            
            if (logInAccount != null)
            {
                Account account = db.Account.Find(logInAccount.AccountId);
                return View(account);
            }

            return RedirectToAction("Login");
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

            ///<summary>
            ///Fills in the friends for the account who logged in
            /// </summary>
            AccountFriends accountFriends = new AccountFriends();
            accountFriends.Account = db.Account.Find(logInAccount.AccountId);
            accountFriends.Friends = db.Friend.Where(a => a.Friend2Id == logInAccount.AccountId).Select(a => a.Account);

            return View(accountFriends);
        }

        public ActionResult RemoveFriend(int id)
        {
            Account logInAccount = Account.LogInAccount;

            ///<summary>
            ///removes friendship
            /// </summary>
            Friend friend = db.Friend.First(a => a.Friend1Id == id & a.Friend2Id == logInAccount.AccountId);
            db.Friend.Remove(friend);

            ///<summary>
            ///removes friendship from the friend as well
            /// </summary>
            Friend friend2 = db.Friend.First(a => a.Friend2Id == id & a.Friend1Id == logInAccount.AccountId);
            db.Friend.Remove(friend2);

            db.SaveChanges();

            return RedirectToAction("FriendsAccount", "Accounts");
        }

        public ActionResult FriendsToChallenge()
        {
            ///<summary>
            ///Check if the user is already logged in 
            /// </summary>
            Account logInAccount = Account.LogInAccount;
            if (logInAccount == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            ///<summary>
            ///Fills in the friends for the account who logged in
            /// </summary>
            AccountFriends accountFriends = new AccountFriends();
            accountFriends.Account = db.Account.Find(logInAccount.AccountId);
            accountFriends.Friends = db.Friend.Where(a => a.Friend2Id == logInAccount.AccountId).Select(a => a.Account);

            return View(accountFriends);
        }

        public ActionResult CreateChallenge(int id)
        {
            Friend.FriendToChallenge = db.Account.Find(id);

            return RedirectToAction("Create", "Challenges");
        }
    }
}
