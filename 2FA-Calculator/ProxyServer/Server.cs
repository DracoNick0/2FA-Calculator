using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ProxyServer
{
    class Server
    {
        private static string userCredeintialsStorageFilePath = @"../../../ServerSide/UserCredentialsStorage.txt";
        private DynamicStorageManager dynamicStorageManager;
        private PersistentStorageManager persistentStorageManager;
        private UserAuthenticator userAuthenticator;
        private PersistentStorageManager userManager;
        private Email2FA email2FA;

        public Server()
        {
            this.persistentStorageManager = new PersistentStorageManager(userCredeintialsStorageFilePath);
            Dictionary<string, Dictionary<string, string>>? tempDynaStorage = this.persistentStorageManager.PopulateDynamicStorage();
            if (tempDynaStorage != null)
            {
                this.dynamicStorageManager = new DynamicStorageManager(tempDynaStorage);
            }
            else
            {
                this.dynamicStorageManager = new DynamicStorageManager(new Dictionary<string, Dictionary<string, string>>());
            }

            this.userAuthenticator = new UserAuthenticator(this.dynamicStorageManager);
            this.userManager = new PersistentStorageManager(userCredeintialsStorageFilePath);
            this.email2FA = new Email2FA(this.dynamicStorageManager);
        }

        public bool CreateAccount(string username, string password, string email)
        {
            return this.dynamicStorageManager.CreateAccount(username, password, email);
        }

        public string AuthenticateUserAndPass(string username, string password)
        {
            string authMessage = this.userAuthenticator.AuthenticateUserAndPass(username, password);

            // If account gets locked, save this.
            if (authMessage.CompareTo("Incorrect credentials!") == 0)
            {
                this.SaveAllUsersCredentials();
                return "Incorrect credentials!";
            }

            return authMessage;
        }

        public bool SendOTPEmail(string userOrEmail)
        {
            return this.email2FA.SendOTPEmail(userOrEmail);
        }

        public bool AuthenticateOTPEmail(string userInput)
        {
            return this.email2FA.AuthenticateOTP(userInput);
        }

        public bool UpdatePassword(string userOrEmail, string newPassword)
        {
            return this.dynamicStorageManager.UpdatePassword(userOrEmail, newPassword);
        }

        public bool UserExists(string username)
        {
            return this.dynamicStorageManager.UserExists(username);
        }

        public bool SaveAllUsersCredentials()
        {
            return this.persistentStorageManager.SaveAllUsersCredentials();
        }
    }
}
