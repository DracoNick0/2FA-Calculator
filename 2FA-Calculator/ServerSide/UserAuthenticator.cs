namespace _2FA_Calculator.ServerSide
{
    class UserAuthenticator
    {
        private DynamicStorageManager dynamicStorageManager;
        public UserAuthenticator(DynamicStorageManager dynamicStorageManager)
        {
            this.dynamicStorageManager = dynamicStorageManager;
        }

        // Add the ability to count incorrect attempts and return "Too many incorrect attempts!" ***********************
        public string AuthenticateUserAndPass(string username, string password)
        {
            Hasher hasher = new Hasher();
            Dictionary<string, string> userDeets = this.dynamicStorageManager.GetUserDetails(username);

            string inputtedPasswordHashed = hasher.computeSha256Hash(password + userDeets["salt"]);
            if (inputtedPasswordHashed.CompareTo(userDeets["hashedPassword"]) == 0)
            {
                return "User verified!";
            }

            return "Incorrect credentials!";
        }
    }
}
