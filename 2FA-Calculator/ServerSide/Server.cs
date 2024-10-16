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

        public bool createAccount(string username, string password, string email)
        {
            return this.userManager.createAccount(username, password, email);
        }

        public bool authenticateUserAndPass(string username, string password)
        {
            return this.userAuthenticator.authenticateUserAndPass(username, password);
        }

        public bool sendOTPEmail(string userOrEmail)
        {
            return this.email2FA.sendOTPEmail(userOrEmail);
        }

        public bool authenticateOTPEmail(string userInput)
        {
            return this.email2FA.authenticateOTP(userInput);
        }

        public void updatePassword(string userOrEmail, string newPassword)
        {
            this.userManager.updatePassword(userOrEmail, newPassword);
        }

        public bool userExists(string username)
        {
            return userManager.userExists(username);
        }
    }
}
