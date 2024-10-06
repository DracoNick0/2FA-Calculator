using System.Text;

namespace _2FA_Calculator.Server
{
    class UserManager
    {
        private Hasher hasher;
        private string storageFilePath;

        public UserManager(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
            this.hasher = new Hasher();
        }

        public bool createAccount(string username, string password)
        {
            if (!userExists(username))
            {
                int saltLength = 8;
                string salt = generateSalt(saltLength);
                string hashedPassword = hasher.computeSha256Hash(password + salt);

                using (StreamWriter sw = new StreamWriter(storageFilePath, true))
                {

                    string line = username + "," + hashedPassword + "," + salt;
                    sw.WriteLine(line);
                    sw.Flush();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool userExists(string username)
        {
            string? line = string.Empty;

            // Read through the user storage
            using (StreamReader sr = new StreamReader(storageFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    // Check if this line contains the username
                    if (tokens[0].CompareTo(username) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string generateSalt(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
