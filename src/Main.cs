using AtmFunctions;
using StoreCard;

namespace MainProgram{
    public class MainProgram{

        public static void Main(string[] args)
        {
            string getUserLogin = Environment.UserName.ToUpper();

            DebitCard.DebitCard genCard = new DebitCard.DebitCard
            {
                CardNumber = new Random().Next(100000000, 999999999).ToString(),
                CardHolderName = getUserLogin,
                ExpiryDate = "12/25",
                CVV = new Random().Next(100, 999).ToString(),
                PIN = new Random().Next(1000, 9999).ToString()
            };
            string UserCardInformation = "Card details generated successfully \n" +
                                    $"Card Holder Name: {genCard.CardHolderName}\n" +
                                    $"Card Number: {genCard.CardNumber}\n" +
                                    $"PIN: {genCard.PIN}\n" +
                                    $"Expiry Date: {genCard.ExpiryDate}\n" +
                                    $"CVV: {genCard.CVV}";
                                  
            Console.WriteLine($"Welcome {getUserLogin} to ATM Machine \n"+
                                "Please press \"Enter Key\" your card details to proceed or type 'gen' to generate a card \n");
            Console.Write("Input:");
            string? userInput = Console.ReadLine();
            
            if (userInput is "gen")
            {
                var sqlShit = new SQLShit();
                Console.WriteLine("Generating a card for you...");
                Thread.Sleep(2000);
                Console.WriteLine(UserCardInformation);
                sqlShit.InsertData(genCard.CardHolderName, genCard.CardNumber,genCard.PIN, genCard.ExpiryDate, genCard.CVV);
                AtmOptions();
            }
            else
            {
                try
                {
                    var sqlShit = new SQLShit();
                    string? cardNumber, cardHolderName, expiryDate, cvv, pin;
                    ManualData(out cardNumber, out cardHolderName, out expiryDate, out cvv, out pin);
                    var CardInfo = new DebitCard.DebitCard
                    {
                        CardHolderName = cardHolderName?.Length < 3 ? throw new Exception("Card holder name must be at least 3 characters long") : cardHolderName,
                        CardNumber = cardNumber?.Length < 5 ? throw new Exception("Card number must be at least 5 characters long") : cardNumber,
                        ExpiryDate = expiryDate?.Length < 5 ? throw new Exception("Expiry date must be at least 5 characters long") : expiryDate,
                        CVV = cvv?.Length < 3 ? throw new Exception("CVV must be at least 3 characters long") : cvv,
                        PIN = pin?.Length < 4 ? throw new Exception("PIN must be at least 4 characters long") : pin
                    };
                    
                    Console.WriteLine("Card details saved successfully \n" +
                                      $"Card Number: {CardInfo.CardNumber}\n" +
                                      $"Card Holder Name: {CardInfo.CardHolderName}\n" +
                                      $"PIN: {CardInfo.PIN}\n" +
                                      $"Expiry Date: {CardInfo.ExpiryDate}\n" +
                                      $"CVV: {CardInfo.CVV}");
                    sqlShit.InsertData(CardInfo.CardHolderName ?? string.Empty,
                                        CardInfo.CardNumber ?? string.Empty,
                                        CardInfo.ExpiryDate ?? string.Empty,
                                        CardInfo.CVV ?? string.Empty,
                                        CardInfo.PIN ?? string.Empty);

                    AtmOptions();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}, Please try again.\n");
                    Main(args);
                }
            }

            static void ManualData(out string? cardNumber, out string? cardHolderName, out string? expiryDate, out string? cvv, out string? pin)
            {
                Console.WriteLine("Please enter your card number");
                cardNumber = Console.ReadLine();
                Console.WriteLine("Please enter your card pin");
                pin = Console.ReadLine();
                Console.WriteLine("Please enter your card holder name");
                cardHolderName = Console.ReadLine();
                Console.WriteLine("Please enter your card expiry date");
                expiryDate = Console.ReadLine();
                Console.WriteLine("Please enter your card CVV");
                cvv = Console.ReadLine();
            }
        }

        public static void AtmOptions()
        {
            RunAtmFunction.RunATM();
        }
    }
}