using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BodyBalance.Persistence;


namespace BodyBalance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DbBodyBalance dao = new DbBodyBalance();
            USER1 u = dao.USER1.Find("Lpisa");
            ViewBag.Title = u.USER_FIRSTNAME.ToString() + " " + u.USER_LASTNAME.ToString();
            return View();
        }
    }
}
