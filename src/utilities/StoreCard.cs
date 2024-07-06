using System.Data.SQLite;
using System.Data.SqlTypes;
using Microsoft.VisualBasic;
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
        internal SQLiteConnection DBconnection()
        {
            string DbName = "creditcard.db";
            string ConnectionString = $"Data Source={DbName};Version=3;";
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
        internal SQLiteCommand DBcommand()
        {
            var command = DBconnection();
            return command.CreateCommand();
        }

        internal void SQLiteCreateTable(){
            try
            {
                var command = DBcommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS CreditCardDetails (CardholderName TEXT, CardNumber TEXT, ExpiryDate TEXT, CVV TEXT);";
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                var command = DBcommand();
                command.CommandText = "DROP TABLE IF EXISTS CreditCardDetails;";
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
        public void InsertData(string cardholderName, string cardNumber, string expiryDate, string cvv)
        {
            SQLiteCreateTable();
            string[] value_parameter = { cardholderName, cardNumber, expiryDate, cvv };
            string[] parameter_name = { "@CardholderName", "@CardNumber", "@ExpiryDate", "@CVV"};
            var command = DBcommand();
            command.CommandText = "INSERT INTO CreditCardDetails (CardholderName, CardNumber, ExpiryDate, CVV) VALUES (@CardholderName, @CardNumber, @ExpiryDate, @CVV)";
            for (int i = 0, j = 0; i < value_parameter.Length && j < parameter_name.Length; i++, j++)
            {
                command.Parameters.Add(new SQLiteParameter(parameter_name[j], value_parameter[i]));
            }
            command.ExecuteNonQuery();
            command.Dispose();
        }


    }

}