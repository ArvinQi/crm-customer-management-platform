using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CRM.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            // 排列
            HomeController controller = new HomeController();

            // 操作
            string result = controller.Login("jaylonqi","123").Content;

            // 断言
            Assert.IsNotNull(result);
            //Assert.AreEqual("登陆成功！", result);
        }
    }
}
