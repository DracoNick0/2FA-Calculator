using _2FA_Calculator.ClientSide;

namespace _2FA_Calculator.ServerSide
{
    class Server
    {
        UserAuthenticator userAuthenticator;
        UserManager userManager;
        Email2FA email2FA;

        public Server()
        {
            this.userAuthenticator = new UserAuthenticator(@"../../../ServerSide/UserCredentialsStorage.txt");
            this.userManager = new UserManager(@"../../../ServerSide/UserCredentialsStorage.txt");
            this.email2FA = new Email2FA();
        }

        public bool createAccount(string username, string password)
        {
            return this.userManager.createAccount(username, password);
        }

        public bool authenticateUserAndPass(string username, string password)
        {
            return userAuthenticator.authenticateUserAndPass(username, password);
        }

        public bool sendOTPEmail(string recieverEmail)
        {
            return email2FA.sendOTPEmail(recieverEmail);
        }

        public bool authenticateOTPEmail(string userInput)
        {
            return email2FA.authenticateOTP(userInput);
        }
    }
}
