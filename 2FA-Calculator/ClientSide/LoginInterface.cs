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

            string authMessage = string.Empty;
            if ((authMessage = this.AuthenticateUserAndPass()).CompareTo("User verified!") == 0)
            {
                string authMethod;
                if ((authMethod = this.server.GetUserAuthMethod(this.username)) == "email")
                {
                    if (this.server.SendOTPEmail(this.username))
                    {
                        Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");
                        if (this.server.AuthenticateOTPEmail(this.requester.RequestInput("OTP")))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else if (authMethod == "google")
                {
                    return this.server.AuthenticateGoogleUsername(username, GoogleAuthenticator.AuthenticateUser());
                }
            }
            else if (authMessage.CompareTo("Incorrect credentials!") == 0)
            {
                Console.Clear();
                Console.WriteLine("Incorrect username and/or password! ;n;");
                Console.WriteLine("Username input: " + this.username);
                Console.WriteLine("Password input: " + this.password);
                Console.WriteLine("This account is locked for 3 minutes!\n");
            }
            else
            {
                Console.WriteLine(authMessage);
            }

            return false;
        }

        public string TwoFactorAuthOptions()
        {
            string? userInput = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("What would you like to login with?");
                Console.WriteLine("[G] Google | [E] Email");

                userInput = Console.ReadLine();
                while (userInput == null)
                {
                    userInput = Console.ReadLine();
                }

                userInput.ToLower();

                switch (userInput)
                {
                    case "g":
                        return GoogleAuthenticator.AuthenticateUser();
                    case "e":
                        this.email = this.requester.RequestInput("email");
                        if (this.server.SendOTPEmail(this.email))
                        {
                            Console.WriteLine("We sent an email to " + this.email + ". Mail may be in junk.");
                            if (this.server.AuthenticateOTPEmail(this.requester.RequestInput("OTP")))
                            {
                                return this.email;
                            }
                        }
                        return string.Empty;
                }
            }
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
                    Console.Clear();
                    Console.WriteLine("Username already exists, try another.");
                }
            }

            Console.Clear();
            this.password = this.requester.RequestInputAndConf("password");


            string authMethod; // This variable either holds users email or google username, either will be used for authentication in the future
            if ((authMethod = TwoFactorAuthOptions()) == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Account creation failed try again!\n");
                return false;
            }
            else
            {
                // Go through server to create account, save the account with the users email or google username.
                this.server.CreateAccount(this.username, this.password, authMethod);
                Console.Clear();
                Console.WriteLine("Account creation successfull!\n");
                return true;
            }
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
                Console.WriteLine("User authentication failed, otp was incorrect.\n");
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
