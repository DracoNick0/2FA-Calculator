namespace _2FA_Calculator.Server
{
    class UserAuthenticator
    {
        string storageFilePath;
        public UserAuthenticator(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
        }

        // Put this in a different file and add security.
        public bool authenticateUser(string username, string password)
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
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
