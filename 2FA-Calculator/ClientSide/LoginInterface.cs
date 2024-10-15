using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ClientSide
{
    class LoginInterface
    {
        private Server server;
        private string inputtedUsername;
        private string inputtedPassword;

        public LoginInterface()
        {
            this.server = new Server();
            this.inputtedUsername = string.Empty;
            this.inputtedPassword = string.Empty;
        }

        public string InputtedUsername()
        { 
            return this.inputtedUsername;
        }

        public bool login()
        {
            requestUserAndPass();

            if (authenticateUserAndPass())
            {
                if (this.server.sendOTPEmail("santos_nick@outlook.com"))
                {
                    if (this.server.authenticateOTPEmail(requestInputAndConf("OTP")))
                    {
                        return true;
                    }
                }
            }
            
            Console.WriteLine("Username input: " + this.inputtedUsername);
            Console.WriteLine("Password input: " + this.inputtedPassword);
            Console.WriteLine("Incorrect username and/or password! ;n;\n");

            return false;
        }

        // Put this in a different file so that we can salt it.
        public void createAccount()
        {
            this.inputtedUsername = requestInputAndConf("username");
            this.inputtedPassword = requestInputAndConf("password");

            // Go through server to create account
            server.createAccount(inputtedUsername, inputtedPassword);
        }

        private void requestUserAndPass()
        {
            Console.Write("Input username,password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                string[] tokens = userInput.Split(',');

                if (tokens.Length == 2)
                {
                    this.inputtedUsername = tokens[0];
                    this.inputtedPassword = tokens[1];
                }
                else
                {
                    Console.WriteLine("\nIncorrect input, try again.");
                    requestUserAndPass();
                }
            }
        }

        private bool authenticateUserAndPass()
        {
            return server.authenticateUserAndPass(inputtedUsername, inputtedPassword);
        }

        private string requestInputAndConf(string nameOfInput)
        {
            bool userIsSatisfied = false;
            string? userInput = null;

            while (!userIsSatisfied || userInput == null || userInput == string.Empty)
            {
                while (userInput == null || userInput == string.Empty)
                {
                    Console.Write("Enter your desired " + nameOfInput + ": ");
                    userInput = Console.ReadLine();

                    // Check if username already exists ********************************************************
                    if (userInput == null || userInput == string.Empty)
                    {
                        Console.WriteLine("\n" + nameOfInput + " not valid, try again.");
                    }
                }

                do
                {
                    Console.WriteLine("Is \"" + userInput + "\" correct?");
                } while (!(userIsSatisfied = assessYesNoInput(Console.ReadLine())));
            }

            return userInput;
        }

        private void requestUsername()
        {
            Console.Write("Input username: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                this.inputtedUsername = userInput;
            }
            else
            {
                requestUsername();
            }
        }

        private void requestPassword()
        {
            Console.Write("Input password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                this.inputtedPassword = userInput;
            }
            else
            {
                requestPassword();
            }
        }

        private bool assessYesNoInput(string? input)
        {
            if (input != null)
            {
                if (input.Length == 1)
                {
                    if (input.CompareTo("y") == 0 || input.CompareTo("Y") == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
