namespace AtmFunctions
{
    public class AtmOptions{
        private int _balance = 1000;
        public int Balance { get => _balance; set => _balance = Balance; }

    }

    public class RunFunctions{

        public static void RunATM(){
            string ProceedMessage = "\nNow that you have your card details, you can proceed to the ATM Machine \n"+
                                    "Please press \"Enter Key\" to proceed.\n";
            int Balance = new AtmOptions().Balance;
            Console.WriteLine(ProceedMessage);
            Console.ReadLine();
            while(true){
                Console.WriteLine("Please select an option: \n" +
                              "1. Check Balance \n" +
                              "2. Withdraw Cash \n" +
                              "3. Deposit Cash \n" +
                              "4. Transfer Funds \n" +
                              "5. Exit \n");
            string? userOption = Console.ReadLine();
            switch (userOption)
            {
                case "1":
                    Console.WriteLine($"Your balance is Rs {Balance}\n");
                    break;
                case "2":
                    Console.WriteLine("Please enter the amount you want to withdraw\n");
                    string? withdrawAmount = Console.ReadLine();
                    if (int.TryParse(withdrawAmount, out int outBalance))
                    {
                        if (Balance >= outBalance )
                        {
                            Balance -= outBalance;
                            Console.WriteLine($"Please take your cash: Rs {outBalance}");

                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds in your account\n");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Please enter the amount you want to deposit\n");
                    string? depositAmount = Console.ReadLine();
                    if (int.TryParse(depositAmount, out int deposit))
                    {
                        Balance += deposit;
                        Console.WriteLine($"You have successfully deposited Rs {deposit}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered");
                    }
                    break;
                case "4":
                    Console.Write("Please enter the account number you want to transfer funds to:");
                    string ? accountNumber= Console.ReadLine();
                    
                    if (accountNumber?.Length <= 10) {
                        Console.WriteLine("Invalid account number entered\n");
                        break;
                    }
                    Console.Write("Please enter the amount you want to transfer:");
                    string? transferAmount = Console.ReadLine();
                    if (int.TryParse(transferAmount, out int transfer) && Balance >= transfer)
                    {
                        Balance -= transfer;
                        Console.WriteLine($"You have successfully transferred Rs {transfer} to account number {accountNumber}\n");
                    }
                    else
                    {   
                        Console.WriteLine($"Insufficient account balance your balance is Rs {Balance}\n");
                    }
                    break;
                case "5":
                    Console.WriteLine("Thank you for using our ATM");
                    break;
                default:
                    Console.WriteLine("Invalid option selected");
                    break;
            }
            }
        }
    }

}