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

        public bool Login()
        {
            this.RequestUserAndPass();

            if (this.AuthenticateUserAndPass() == "User verified!")
            {
                if (this.server.SendOTPEmail(this.username))
                {
                    Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");
                    if (this.server.AuthenticateOTPEmail(this.requester.RequestInput("OTP")))
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

        public bool CreateAccount()
        {
            string? userInput = string.Empty;
            bool userExists = true;

            while (userExists)
            {
                this.username = this.requester.RequestInputAndConf("username");
                if (!this.server.UserExists(this.username))
                {
                    userExists = false;
                }
                else
                {
                    Console.WriteLine("Username already exists, try another.");
                }
            }

            this.password = this.requester.RequestInputAndConf("password");

            // Make the following code the servers responsibility for higher security **********************************************************************************************
            this.server.SendOTPEmail(this.email = this.requester.RequestInputAndConf("email"));
            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");

            Console.Write("Please input the otp: ");
            userInput = Console.ReadLine();

            if (userInput == null || !this.server.AuthenticateOTPEmail(userInput))
            {
                Console.Clear();
                Console.WriteLine("Account creation failed, otp was incorrect.");
                return false;
            }


            // Go through server to create account
            this.server.CreateAccount(this.username, this.password, this.email);
            Console.Clear();
            Console.WriteLine("Account creation successfull!");
            return true;
        }

        // Make this function more secure, by making the client side have to send a code to the server along side the new password, for the password to be saved. ********************************
        public bool ForgotLogin()
        {
            string? userOrEmail = string.Empty;
            string? userInput = string.Empty;

            this.server.SendOTPEmail(userOrEmail = this.requester.RequestInputAndConf("username or email"));
            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");

            Console.Write("Please input the otp: ");
            userInput = Console.ReadLine();

            if (userInput == null || !this.server.AuthenticateOTPEmail(userInput))
            {
                Console.WriteLine("User authentication failed, otp was incorrect.");
                return false;
            }

            this.server.UpdatePassword(userOrEmail, this.requester.RequestInputAndConf("new password"));
            return true;
        }

        private void RequestUserAndPass()
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
                    RequestUserAndPass();
                }
            }
        }

        private string AuthenticateUserAndPass()
        {
            return this.server.AuthenticateUserAndPass(this.username, this.password);
        }

        public bool SaveAllUsersCredentials()
        {
            return this.server.SaveAllUsersCredentials();
        }
    }
}
