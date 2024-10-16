namespace _2FA_Calculator.ClientSide
{
    class RequestFromUserMethods
    {
        public string requestInput(string nameOfInput)
        {
            string? userInput = null;

            while (userInput == null || userInput == string.Empty)
            {
                Console.Write("Enter your desired " + nameOfInput + ": ");
                userInput = Console.ReadLine();

                if (userInput == null || userInput == string.Empty)
                {
                    Console.Write("\n" + nameOfInput + " not valid, try again: ");
                }
            }

            return userInput;
        }

        public string requestInputAndConf(string nameOfInput)
        {
            bool userIsSatisfied = false;
            string? userInput = null;

            while (!userIsSatisfied)
            {
                while (userInput == null || userInput == string.Empty)
                {
                    Console.Write("Enter your desired " + nameOfInput + ": ");
                    userInput = Console.ReadLine();

                    if (userInput == null || userInput == string.Empty)
                    {
                        Console.WriteLine("\n" + nameOfInput + " not valid, try again.");
                    }
                }

                userIsSatisfied = confirmIfInputIsCorrect(userInput);
            }

            return userInput;
        }

        public bool confirmIfInputIsCorrect(string input)
        {
            Console.WriteLine("Is \"" + input + "\" correct?");
            return confirm();
        }

        public bool confirm()
        {
            string? input;
            while (true)
            {
                input = Console.ReadLine();

                if (input != null)
                {
                    if (input.Length == 1)
                    {
                        if (input.CompareTo("y") == 0 || input.CompareTo("Y") == 0)
                        {
                            return true;
                        }
                        else if (input.CompareTo("n") == 0 || input.CompareTo("N") == 0)
                        {
                            return false;
                        }
                    }
                }

                Console.Write("Please enter y or n: ");
            }
        }
    }
}
