using CRM.DataBase;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        Database db = new Database();
        //
        // GET: /Portal/Home/

        public ActionResult Index()
        {
            ViewData["currentTag"] = "Clients";
            return View();
        }
        public ActionResult Index1()
        {
            ViewData["currentTag"] = "Clients";
            return View();
        }
        public ActionResult Personal(string id)
        {
            ViewData["currentTag"] = "Clients";
            if (Session["currentUser"] == null)
            {
                Response.Redirect("~/Home/Index");
            }
            tb_Users user = db.GetUserInfo((Session["currentUser"] as tb_Users).UserID);
            return View(user);
        }
        public ActionResult Password(string id)
        {
            ViewData["currentTag"] = "Clients";
            if (Session["currentUser"] == null)
            {
                Response.Redirect("~/Home/Index");
            }
            tb_Users user = db.GetUserInfo((Session["currentUser"] as tb_Users).UserID);
            return View(user);
        }

    }
}
