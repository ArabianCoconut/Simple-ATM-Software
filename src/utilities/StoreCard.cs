using System.Data.SQLite;
using System.Data.SqlTypes;
namespace StoreCard
{
    public class SQLShit
    {
        public string? ConnectionString { get; set; }
        public string? Query { get; set; }
        public string ExecuteQuery()
        {
            return "Query executed successfully";
        }

        internal void SQLiteCreateTable(){
            string DbName = "creditcard.db";
            try
            {
                string connectionString = $"Data Source={DbName};Version=3;";
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                using var command = new SQLiteCommand(connection);
                command.CommandText = "CREATE TABLE CreditCardDetails (Id INTEGER PRIMARY KEY, CardholderName TEXT, CardNumber TEXT, ExpiryDate TEXT, CVV TEXT)";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                string connectionString = $"Data Source={DbName};Version=3;";
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                using var command = new SQLiteCommand(connection);
                command.CommandText = "DROP TABLE IF EXISTS CreditCardDetails;";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }
        public void InsertData(string cardholderName, string cardNumber, string expiryDate, string cvv)
        {
            SQLiteCreateTable();
            string[] value_parameter = { cardholderName, cardNumber, expiryDate, cvv };
            string[] parameter_name = { "@CardholderName", "@CardNumber", "@ExpiryDate", "@CVV"};
            string connectionString = $"Data Source=creditcard.db;Version=3;";
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            using var command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO CreditCardDetails (CardholderName, CardNumber, ExpiryDate, CVV) VALUES (@CardholderName, @CardNumber, @ExpiryDate, @CVV)";
            for (int i = 0, j = 0; i < value_parameter.Length && j < parameter_name.Length; i++, j++)
            {
                command.Parameters.Add(new SQLiteParameter(parameter_name[j], value_parameter[i]));
            }
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }


    }

}