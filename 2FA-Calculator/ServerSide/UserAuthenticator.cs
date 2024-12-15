namespace _2FA_Calculator.ServerSide
{
    class UserAuthenticator
    {
        private DynamicStorageManager dynamicStorageManager;
        private static int lockoutTime = 3; // In minutes
        public UserAuthenticator(DynamicStorageManager dynamicStorageManager)
        {
            this.dynamicStorageManager = dynamicStorageManager;
        }

        // Add the ability to count incorrect attempts and return "Too many incorrect attempts!" ***********************
        public string AuthenticateUserAndPass(string username, string password)
        {
            Dictionary<string, string> userDeets = this.dynamicStorageManager.GetUserDetails(username);
            DateTime userWasLastLockedOut = DateTime.Parse(userDeets["time when locked out"]);

            // If it it's been longer than 3 minutes since user was last locked out.
            if ((DateTime.Now - userWasLastLockedOut).TotalMinutes > lockoutTime)
            {
                Hasher hasher = new Hasher();
                string inputtedPasswordHashed = hasher.computeSha256Hash(password + userDeets["salt"]);
                if (inputtedPasswordHashed.CompareTo(userDeets["hashed password"]) == 0)
                {
                    return "User verified!";
                }
                else
                {
                    userDeets["time when locked out"] = DateTime.Now.ToString();
                    return "Incorrect credentials!";
                }
            }
            else
            {
                int minutesTillFree = lockoutTime - (int)Math.Ceiling((DateTime.Now - userWasLastLockedOut).TotalMinutes);
                int secondsTillFree = (lockoutTime * 60) - (int)Math.Ceiling((DateTime.Now - userWasLastLockedOut).TotalSeconds) - (minutesTillFree * 60);
                return (minutesTillFree + " minutes " + secondsTillFree + " seconds until account is no longer locked!");
            }
        }

        public bool AuthenticateGoogleUsername(string username, string gUsername)
        {
            Dictionary<string, string> userDeets = this.dynamicStorageManager.GetUserDetails(username);
            if (gUsername.CompareTo(userDeets["auth"]) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
