using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Controllers.API
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void OrderControllerTest_One()
        {
            var orderController = new OrderController();

            var orders = orderController.GetOrders(1);

            Assert.AreEqual(3, orders.Count());
        }

        [TestMethod]
        public void OrderControllerTest_Two()
        {
            var orderController = new OrderController();

            var orders = orderController.GetOrders(2);

            Assert.AreEqual(0, orders.Count());
        }
    }
}
