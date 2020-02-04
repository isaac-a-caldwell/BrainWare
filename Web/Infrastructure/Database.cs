namespace Web.Infrastructure
{
    using System.Data.Common;
    using System.Data.SqlClient;

    public class Database
    {
        private readonly SqlConnection _connection;
        private static readonly string _defaultConnectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;" + 
            "Initial Catalog = BrainWare; Integrated Security = SSPI;" + 
            "AttachDBFilename=C:\\dev\\BrainWare-master\\Web\\App_Data\\BrainWare.mdf";

        public Database() : this(_defaultConnectionString)
        {
        }

        public Database(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

            _connection.Open();
        }


        public DbDataReader ExecuteReader(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

    }
}