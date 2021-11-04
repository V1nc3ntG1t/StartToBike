using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StartToBike.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Leaderboard()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //int kilomter = 10;
        //public void Vooruitgang(int kilometer)
        //{
        //    if (true)
        //    {
        //        VooruitgangTxt.Text = 'laag';
        //    }
        //}
    }
}