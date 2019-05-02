using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            ViewBag.Error = "page you want to redirect to no longer exists";
            return View("~/Views/Course/Error.cshtml");
        }
    }
}