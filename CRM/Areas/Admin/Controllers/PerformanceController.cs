using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    public class PerformanceController : Controller
    {
        //
        // GET: /Admin/Performance/

        public ActionResult PersonalPerformance()
        {
            return View();
        }

    }
}
