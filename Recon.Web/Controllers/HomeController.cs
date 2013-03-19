using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recon.Web.Models;

namespace Recon.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            String temp = Request.Browser.Browser;
            if (Request.Browser.Browser.ToUpper() == "IE" && Request.Browser.MajorVersion < 8)
            {
                return View("UpgradeYourBrowser");
            }
            if (Request.Browser.Browser.ToUpper() == "FIREFOX" && Request.Browser.MajorVersion < 4)
            {
                return View("UpgradeYourBrowser");
            }
            return View();
        }
    }
}
