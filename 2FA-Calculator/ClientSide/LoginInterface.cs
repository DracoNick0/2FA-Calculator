using _2FA_Calculator.ProxyServer;

namespace _2FA_Calculator.ClientSide
{
    class LoginInterface
    {
        private Server server;
        private RequestFromUserMethods requester;
        private string username;
        private string password;
        private string email;

        public LoginInterface()
        {
            this.server = new Server();
            this.requester = new RequestFromUserMethods();
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
            this.requestUserAndPass();

            if (this.authenticateUserAndPass() == "User verified!")
            {
                if (this.server.sendOTPEmail(this.username))
                {
                    Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");
                    if (this.server.authenticateOTPEmail(this.requester.requestInput("OTP")))
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

        public bool createAccount()
        {
            string? userInput = string.Empty;
            bool userExists = true;

            while (userExists)
            {
                this.username = this.requester.requestInputAndConf("username");
                if (!this.server.userExists(this.username))
                {
                    userExists = false;
                }
                else
                {
                    Console.WriteLine("Username already exists, try another.");
                }
            }

            this.password = this.requester.requestInputAndConf("password");

            // Make the following code the servers responsibility for higher security **********************************************************************************************
            this.server.sendOTPEmail(this.email = this.requester.requestInputAndConf("email"));
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

        // Make this function more secure, by making the client side have to send a code to the server along side the new password, for the password to be saved. ********************************
        public bool forgotLogin()
        {
            string? userOrEmail = string.Empty;
            string? userInput = string.Empty;

            this.server.sendOTPEmail(userOrEmail = this.requester.requestInputAndConf("username or email"));
            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");

            Console.Write("Please input the otp: ");
            userInput = Console.ReadLine();

            if (userInput == null || !this.server.authenticateOTPEmail(userInput))
            {
                Console.WriteLine("User authentication failed, otp was incorrect.");
                return false;
            }

            this.server.updatePassword(userOrEmail, this.requester.requestInputAndConf("new password"));
            return true;
        }

        private void requestUserAndPass()
        {
            Console.WriteLine("Input your \"username password\".");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                string[] tokens = userInput.Split(' ');

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

        private string authenticateUserAndPass()
        {
            return server.authenticateUserAndPass(this.username, this.password);
        }
    }
}
