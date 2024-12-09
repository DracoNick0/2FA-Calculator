using _2FA_Calculator.ServerSide;

namespace _2FA_Calculator.ProxyServer
{
    class Server
    {
        private static string userCredeintialsStorageFilePath = @"../../../ServerSide/UserCredentialsStorage.txt";
        private DynamicStorageManager dynamicStorageManager;
        private PersistentStorageManager persistentStorageManager;
        private UserAuthenticator userAuthenticator;
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
            this.email2FA = new Email2FA(this.dynamicStorageManager);
        }

        public bool CreateAccount(string username, string password, string email)
        {
            bool returnValue =  this.dynamicStorageManager.CreateAccount(username, password, email);
            this.SaveAllUsersCredentials();
            return returnValue;
        }

        public string AuthenticateUserAndPass(string username, string password)
        {
            string authMessage = string.Empty;

            if (this.UserExists(username))
            {
                authMessage = this.userAuthenticator.AuthenticateUserAndPass(username, password);

                // If account gets locked, save this.
                if (authMessage.CompareTo("Incorrect credentials!") == 0)
                {
                    this.SaveAllUsersCredentials();
                }
            }
            else
            {
                authMessage = "User doesn't exist!";
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
            bool returnValue = this.dynamicStorageManager.UpdatePassword(userOrEmail, newPassword);
            this.SaveAllUsersCredentials();
            return returnValue;
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
