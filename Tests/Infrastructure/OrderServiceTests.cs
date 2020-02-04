using Web.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Infrastructure
{
    [TestClass]
    public class OrderServiceTests
    {
        [TestMethod]
        public void GetOrdersByCompanyId_One()
        {
            //realistically would like to have a real test db but for an assessment that seems overkill
            var testDbPath = "C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWare;" +
                $"Integrated Security=SSPI;AttachDBFilename={testDbPath}";
            var database = new Database(connectionString);
            var orderService = new OrderService();

            var results = orderService.GetOrdersForCompany(database, 1);

            //only use magic numbers like this when connecting to strictly controlled test db
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void GetOrdersByCompanyId_Two()
        {
            //realistically would like to have a real test db but for an assessment that seems overkill
            var testDbPath = "C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWare;" +
                $"Integrated Security=SSPI;AttachDBFilename={testDbPath}";
            var database = new Database(connectionString);
            var orderService = new OrderService();

            var results = orderService.GetOrdersForCompany(database, 2);

            //only use magic numbers like this when connecting to strictly controlled test db
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void GetOrderProducts()
        {
            //realistically would like to have a real test db but for an assessment that seems overkill
            var testDbPath = "C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWare;" +
                $"Integrated Security=SSPI;AttachDBFilename={testDbPath}";
            var database = new Database(connectionString);
            var orderService = new OrderService();

            var results = orderService.GetOrderProductsJoinedByProducts(database);

            //only use magic numbers like this when connecting to strictly controlled test db
            Assert.AreEqual(12, results.Count);
        }
    }
}
