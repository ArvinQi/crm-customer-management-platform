﻿using System.Web.Mvc;

namespace CRM.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Portal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Portal_default",
                "Portal/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.Portal.Controllers" }
            );
        }
    }
}
