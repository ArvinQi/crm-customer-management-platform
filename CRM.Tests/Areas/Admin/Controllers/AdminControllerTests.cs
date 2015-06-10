using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
namespace CRM.Areas.Admin.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {
        [TestMethod()]
        public void UserInfoTest()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.UserInfo("100002") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Gettb_DepartsTest()
        {
            AdminController controller = new AdminController();
            JsonResult result = controller.Gettb_Departs("100002");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetAlltb_DutiesTest()
        {
            AdminController controller = new AdminController();
            JsonResult result = controller.GetAlltb_Duties("100002");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetAlltb_UsersTest()
        {
            AdminController controller = new AdminController();
            JsonResult result = controller.GetAlltb_Users("100002");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Gettb_DutiesTest()
        {
            AdminController controller = new AdminController();
            JsonResult result = controller.Gettb_Duties("100002");
            Assert.IsNotNull(result);
        }
    }
}
