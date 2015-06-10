using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Areas.Portal.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
namespace CRM.Areas.Portal.Controllers.Tests
{
    [TestClass()]
    public class ClientControllerTests
    {
        [TestMethod()]
        public void ClientInfoTest()
        {
            ClientController controller = new ClientController();
            ViewResult result = controller.ClientInfo("100002") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetClientsTest()
        {
            ClientController controller = new ClientController();
            JsonResult result = controller.GetClients("100002");
            Assert.IsNotNull(result);
        }
    }
}
