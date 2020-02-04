using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Infrastructure;

namespace Tests.Infrastructure
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void NoConnectionString()
        {
            try
            {
                var database = new Database();
            }
            catch
            {
                Assert.Fail("Database connection should have been opened successfully with default string.");
            }
        }

        [TestMethod]
        public void ExecuteReader_SelectFromOrdersWithWhere()
        {
            //realistically would like to have a real test db but for an assessment that seems overkill
            var testDbPath = "C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWare;" +
                $"Integrated Security=SSPI;AttachDBFilename={testDbPath}";
            var database = new Database(connectionString);

            var query = "SELECT * FROM \"ORDER\" WHERE order_id = 1;";
            
            var dbReader = database.ExecuteReader(query);

            //select statements return -1
            Assert.AreEqual(-1, dbReader.RecordsAffected);
            while(dbReader.Read())
            {
                Assert.AreEqual("Our first order.", dbReader.GetValue(1));
            }
            dbReader.Close();
        }

        [TestMethod]
        public void ExecuteNonQuery_InsertIntoOrders()
        {
            //realistically would like to have a real test db but for an assessment that seems overkill
            var testDbPath = "C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWare;" +
                $"Integrated Security=SSPI;AttachDBFilename={testDbPath}";
            var database = new Database(connectionString);

            //make sure the test column we add doesn't already exist
            DbCleanup(database);

            var query = "INSERT INTO \"ORDER\"(\"description\", company_id) VALUES('A test order.', 1);";

            var rowsAffected = database.ExecuteNonQuery(query);

            Assert.AreEqual(1, rowsAffected);

            DbCleanup(database);
        }

        private void DbCleanup(Database database)
        {
            database.ExecuteNonQuery("DELETE FROM \"ORDER\" WHERE \"description\"='A test order.';");
        }
    }
}
