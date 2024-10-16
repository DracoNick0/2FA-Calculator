using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ClientSide
{
    class LoginInterface
    {
        private Server server;
        private string username;
        private string password;
        private string email;

        public LoginInterface()
        {
            this.server = new Server();
            this.username = string.Empty;
            this.password = string.Empty;
            this.email = string.Empty;
        }

        public string Username()
        { 
            return this.username;
        }

        public bool login()
        {
            requestUserAndPass();

            if (authenticateUserAndPass())
            {
                if (this.server.sendOTPEmail(this.username))
                {
                    Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");
                    if (this.server.authenticateOTPEmail(requestUserInput("OTP")))
                    {
                        return true;
                    }
                }
            }
            
            Console.WriteLine("Username input: " + this.username);
            Console.WriteLine("Password input: " + this.password);
            Console.WriteLine("Incorrect username and/or password! ;n;\n");

            return false;
        }

        // Put this in a different file so that we can salt it.
        public bool createAccount()
        {
            string? userInput = string.Empty;
            bool userExists = true;

            while (userExists)
            {
                this.username = requestInputAndConf("username");
                if (!this.server.userExists(this.username))
                {
                    userExists = false;
                }
                else
                {
                    Console.WriteLine("Username already exists, try another.");
                }
            }

            this.password = requestInputAndConf("password");

            this.server.sendOTPEmail(this.email = requestInputAndConf("email"));
            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");

            Console.Write("Please input the otp: ");
            userInput = Console.ReadLine();

            if (userInput == null || !this.server.authenticateOTPEmail(userInput))
            {
                Console.WriteLine("Account creation failed, otp was incorrect.");
                return false;
            }


            // Go through server to create account
            this.server.createAccount(this.username, this.password, this.email);
            Console.WriteLine("Account creation successfull!");
            return true;
        }

        // Make this function more secure, by making the client side have to send a code to the server along side the new password, for the password to be saved.
        public bool forgotLogin()
        {
            string? userOrEmail = string.Empty;
            string? userInput = string.Empty;

            this.server.sendOTPEmail(userOrEmail = requestInputAndConf("username or email"));
            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");

            Console.Write("Please input the otp: ");
            userInput = Console.ReadLine();

            if (userInput == null || !this.server.authenticateOTPEmail(userInput))
            {
                Console.WriteLine("User authentication failed, otp was incorrect.");
                return false;
            }

            this.server.updatePassword(userOrEmail, requestInputAndConf("new password"));
            return true;
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
                    this.username = tokens[0];
                    this.password = tokens[1];
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
            return server.authenticateUserAndPass(this.username, this.password);
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

        private string requestUserInput(string requesting)
        {
            Console.Write("Input " + requesting + ": ");

            string? userInput = null;
            while ((userInput = Console.ReadLine()) == null || userInput == string.Empty)
            {
                Console.Write("Broken input, try again: ");
            }
            
            return userInput;
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
