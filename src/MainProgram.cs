﻿using AtmFunctions;
using DebitCard;
using StoreCard;

namespace MainProgram
{
    public class MainProgram
    {
        public static void Main(string[] args)
        /* This method is responsible for running the ATM */
        {
            string getUserLogin = Environment.UserName.ToUpper();
            var _SQL = new SQLDatabase();
            var genCard = new CardDetails();
            string UserCardInformation = "Card details generated successfully \n" +
                                    $"Card Holder Name: {genCard.CardHolderName}\n" +
                                    $"Card Number: {genCard.CardNumber}\n" +
                                    $"PIN: {genCard.PIN}\n" +
                                    $"Expiry Date: {genCard.ExpiryDate}\n" +
                                    $"CVV: {genCard.CVV}";

            Console.Title = "ATM Machine Simulation";
            Console.WriteLine($"Welcome {getUserLogin} to ATM Machine \n" +
                                "Please press \"Enter Key\" your card details to proceed or type 'gen' to generate a card \n");
            Console.Write("Input:");
            string? userInput = Console.ReadLine();

            if (userInput is "gen")
            {
                genCard = new CardDetails();
                Console.WriteLine("Generating a card for you...");
                Thread.Sleep(1000);
                Console.WriteLine(UserCardInformation);

                _SQL.InsertData(genCard.CardHolderName ?? string.Empty,
                                genCard.CardNumber ?? string.Empty,
                                genCard.PIN ?? string.Empty,
                                genCard.ExpiryDate ?? string.Empty,
                                genCard.CVV ?? string.Empty);
                AtmOptions();
            }
            else
            {
                try
                {
                    string? cardNumber, cardHolderName, expiryDate, cvv, pin;
                    ManualData(out cardNumber, out cardHolderName, out expiryDate, out cvv, out pin);
                    var CardInfo = new CardDetails
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
                    
                    _SQL.InsertData(CardInfo.CardHolderName ?? string.Empty,
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
                Console.Write("Please enter your card number:");
                cardNumber = Console.ReadLine();
                Console.Write("Please enter your card pin:");
                pin = Console.ReadLine();
                Console.Write("Please enter your card holder name:");
                cardHolderName = Console.ReadLine();
                Console.Write("Please enter your card expiry date:");
                expiryDate = Console.ReadLine();
                Console.Write("Please enter your card CVV:");
                cvv = Console.ReadLine();
            }
        }

        public static void AtmOptions()
        /* This method is used to display the ATM options */
        {
            RunAtmFunction.RunATM();
        }
    }
}