using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ProxyServer
{
    class Server
    {
        private static string userCredeintialsStorageFilePath = @"../../../ServerSide/UserCredentialsStorage.txt";
        private PersistentStorageManager persistentStorageManager;
        private DynamicStorageManager dynamicStorageManager;
        private UserAuthenticator userAuthenticator;
        private PersistentStorageManager userManager;
        private Email2FA email2FA;

        public Server()
        {
            this.persistentStorageManager = new PersistentStorageManager(userCredeintialsStorageFilePath);
            Dictionary<string, Dictionary<string, string>>? tempDynaStorage = this.persistentStorageManager.populateDynamicStorage();
            if (tempDynaStorage != null)
            {
                this.dynamicStorageManager = new DynamicStorageManager(tempDynaStorage);
            }
            else
            {
                this.dynamicStorageManager = new DynamicStorageManager(new Dictionary<string, Dictionary<string, string>>());
            }

            this.userAuthenticator = new UserAuthenticator(userCredeintialsStorageFilePath);
            this.userManager = new PersistentStorageManager(userCredeintialsStorageFilePath);
            this.email2FA = new Email2FA();
        }

        public bool createAccount(string username, string password, string email)
        {
            return this.dynamicStorageManager.createAccount(username, password, email);
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
            this.dynamicStorageManager.updatePassword(userOrEmail, newPassword);
        }

        public bool userExists(string username)
        {
            return this.dynamicStorageManager.userExists(username);
        }
    }
}
