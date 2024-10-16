using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ProxyServer
{
    class Server
    {
        private static string userCredeintialsStorageFilePath = @"../../../ServerSide/UserCredentialsStorage.txt";
        private UserAuthenticator userAuthenticator;
        private UserManager userManager;
        private Email2FA email2FA;

        public Server()
        {
            this.userAuthenticator = new UserAuthenticator(userCredeintialsStorageFilePath);
            this.userManager = new UserManager(userCredeintialsStorageFilePath);
            this.email2FA = new Email2FA();
        }

        public bool createAccount(string username, string password, string email)
        {
            return this.userManager.createAccount(username, password, email);
        }

        public string authenticateUserAndPass(string username, string password)
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
            return this.userManager.userExists(username);
        }
    }
}
