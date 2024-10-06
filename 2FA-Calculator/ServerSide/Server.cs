namespace _2FA_Calculator.ServerSide
{
    class Server
    {
        UserAuthenticator userAuthenticator;
        UserManager userManager;

        public Server()
        {
            this.userAuthenticator = new UserAuthenticator(@"../../../ServerSide/UserCredentialsStorage.txt");
            this.userManager = new UserManager(@"../../../ServerSide/UserCredentialsStorage.txt");
        }

        public bool createAccount(string username, string password)
        {
            return this.userManager.createAccount(username, password);
        }

        public bool authenticateUser(string username, string password)
        {
            return userAuthenticator.authenticateUser(username, password);
        }
    }
}
