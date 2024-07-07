using StoreCard;

// Unit test for the StoreCard class
SQLShit sqlShit = new SQLShit();
sqlShit.InsertData("John Doe", "0000000000", "1234", "12/22", "123");
sqlShit.InsertData("Jane Doe", "1111111111", "4321", "12/23", "321");
var command = sqlShit.DBcommand();
command.CommandText = "SELECT * FROM CreditCardDetails;";
var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader["CardholderName"]);
    Console.WriteLine(reader["CardNumber"]);
    Console.WriteLine(reader["Pin"]);
    Console.WriteLine(reader["ExpiryDate"]);
    Console.WriteLine(reader["CVV"]);
    Console.WriteLine("\n");
}
reader.Close();
command.Dispose();