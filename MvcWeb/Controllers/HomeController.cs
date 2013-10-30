using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWeb.Controllers
{
    public class HomeController : Controller
    {
        // The AllowAnonymous attribute enables you to white-list the methods you want to opt out of authorization. 
        [AllowAnonymous]
        public ActionResult Index()
        {
            Trace.WriteLine("it's me");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}