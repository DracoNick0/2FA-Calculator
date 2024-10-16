namespace _2FA_Calculator.ServerSide
{
    class UserAuthenticator
    {
        private string storageFilePath;
        public UserAuthenticator(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
        }

        // Add the ability to count incorrect attempts and return "Too many incorrect attempts!" ***********************
        public string authenticateUserAndPass(string username, string password)
        {
            string? line = string.Empty;
            Hasher hasher = new Hasher();

            using (StreamReader sr = new StreamReader(storageFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    string saltedPasswordHash = hasher.computeSha256Hash(password + tokens[2]);
                    if (tokens[0].CompareTo(username) == 0 && tokens[1].CompareTo(saltedPasswordHash) == 0)
                    {
                        return "User verified!";
                    }
                }
            }

            return "Incorrect credentials!";
        }
    }
}
