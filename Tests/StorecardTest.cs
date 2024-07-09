using NUnit.Framework;
using StoreCard;
namespace Tests;


public class Test : StoreCard.SQLShit{
[Test]
public void UpdateDatabase_IsUpdated_ValuesChanged()
{
    // Insert initial data
    InsertData("John Doe", "0000000000", "1234", "12/22", "123");
    // Retrieve the updated data from the database
    var command = DBcommand();
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
}
}