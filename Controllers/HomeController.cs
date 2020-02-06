using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indigo.Controllers
{
    public class HomeController : Controller
    {
        private Indigo.Models.Persons PersonsDb = new Models.Persons();

        public ActionResult Index()
        {
            return View(PersonsDb);
        }

        public ActionResult AddPerson(string f, string m, string l, string email, int type)
        {
            return Json( Indigo.Models.Persons.InsertPerson(f, m, l, email, type));
        }

        public ActionResult DeletePerson(int rank)
        {
            return Json(Indigo.Models.Persons.DeletePerson(rank));
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}