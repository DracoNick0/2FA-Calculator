using System.Collections.Generic;
using System.Text;

namespace _2FA_Calculator.ServerSide
{
    class DynamicStorageManager
    {
        // Username : {hashed password, salt, email}
        private Dictionary<string, Dictionary<string, string>> everyUsersDetails;
        private Hasher hasher;

        public DynamicStorageManager()
        {
            this.everyUsersDetails = new Dictionary<string, Dictionary<string, string>>();
            this.hasher = new Hasher();
        }

        public bool addAccount(string username, string hashedPassword, string salt, string email)
        {
            Dictionary<string, string> aUsersCredentials = new Dictionary<string, string>();

            // If list is not null and user does not yet exists
            // then add user and their credentials.
            if (aUsersCredentials != null && !userExists(username))
            {
                aUsersCredentials.Add("hashedPassword", hashedPassword);
                aUsersCredentials.Add("salt", salt);
                aUsersCredentials.Add("email", email);
                this.everyUsersDetails.Add(username, aUsersCredentials);

                return true;
            }

            return false;
        }

        public string getUserEmail(string username)
        {
            if (userExists(username))
            {
                return this.everyUsersDetails[username]["email"];
            }

            return string.Empty;
        }

        public bool createAccount(string username, string password, string email)
        {
            if (!userExists(username))
            {
                int saltLength = 8;
                string salt = generateSalt(saltLength);
                string hashedPassword = this.hasher.computeSha256Hash(password + salt);

                if (this.everyUsersDetails != null)
                {
                    this.everyUsersDetails[username] = new Dictionary<string, string>();
                    this.everyUsersDetails[username]["hashedPassword"] = hashedPassword;
                    this.everyUsersDetails[username]["salt"] = salt;
                    this.everyUsersDetails[username]["email"] = email;
                }

                return true;
            }

            return false;
        }

        public bool userExists(string username)
        {
            return this.everyUsersDetails.ContainsKey(username);
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
