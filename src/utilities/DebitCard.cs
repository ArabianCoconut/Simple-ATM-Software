namespace DebitCard
{
     struct CardDetails
        {
            public string? CardNumber;
            public string? CardHolderName;
            public string? ExpiryDate;
            public string? CVV;
            public string? PIN;

            public CardDetails()
            {
                CardNumber = new Random().Next(100000000, 999999999).ToString();
                CardHolderName = "John Doe";
                ExpiryDate = "12/25";
                CVV = new Random().Next(100, 999).ToString();
                PIN = new Random().Next(1000, 9999).ToString();
            }
        }
}
