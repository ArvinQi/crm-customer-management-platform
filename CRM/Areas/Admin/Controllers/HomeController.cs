using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            ViewData["currentTag"] = "Index";
            return View();
        }
        public ActionResult Index1()
        {
            ViewData["currentTag"] = "Index";
            return View();
        }

        public ActionResult Error()
        {
            ViewData["Message"] = Request.QueryString["Message"]??"未知错误！";
            ViewData["GoBack"] = "/CRM/Home/Index";
            return View();
        }

    }
}
