using System.Data.SQLite;
namespace StoreCard
{
    public class SQLShit
    {
        public SQLiteConnection DBconnection()
        {
            string DbName = "creditcard.db";
            string ConnectionString = $"Data Source={DbName};Version=3;";
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
        public SQLiteCommand DBcommand()
        {
            var command = DBconnection();
            return command.CreateCommand();
        }

        public void SQLiteCreateTable(){
            try
            {
                var command = DBcommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS CreditCardDetails (CardholderName TEXT, CardNumber TEXT, Pin TEXT, ExpiryDate TEXT, CVV TEXT);";
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
        public void InsertData(string cardholderName, string cardNumber, string Pin, string expiryDate, string cvv)
        {
            SQLiteCreateTable();
            string[] value_parameter = {cardholderName, cardNumber, Pin, expiryDate, cvv};
            string[] parameter_name = {"@CardholderName", "@CardNumber","@Pin" ,"@ExpiryDate", "@CVV"};
            var command = DBcommand();
            command.CommandText = "INSERT INTO CreditCardDetails (CardholderName, CardNumber, Pin, ExpiryDate, CVV) VALUES (@CardholderName, @CardNumber, @Pin, @ExpiryDate, @CVV);";
            for (int i = 0; i < value_parameter.Length; i++)
            {
                command.Parameters.Add(new SQLiteParameter(parameter_name[i], value_parameter[i]));
            }
            command.ExecuteNonQuery();
            command.Dispose();
        }


    }

}