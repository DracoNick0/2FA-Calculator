using System.Collections.Generic;
using System.Text;

namespace _2FA_Calculator.ServerSide
{
    class DynamicStorageManager
    {
        // user : {hashed password, salt, email}
        private Dictionary<string, Dictionary<string, string>> allUsersDeets;
        private Hasher hasher;

        public DynamicStorageManager(Dictionary<string, Dictionary<string, string>> storage)
        {
            this.allUsersDeets = storage;
            this.hasher = new Hasher();
        }

        public bool addAccount(string user, string hashedPassword, string salt, string email)
        {
            Dictionary<string, string> aUsersCredentials = new Dictionary<string, string>();

            // If list is not null and user does not yet exists
            // then add user and their credentials.
            if (aUsersCredentials != null && !userExists(user))
            {
                aUsersCredentials.Add("hashedPassword", hashedPassword);
                aUsersCredentials.Add("salt", salt);
                aUsersCredentials.Add("email", email);
                this.allUsersDeets.Add(user, aUsersCredentials);

                return true;
            }

            return false;
        }

        public string getUserEmail(string user)
        {
            if (userExists(user))
            {
                return this.allUsersDeets[user]["email"];
            }

            return string.Empty;
        }

        public bool createAccount(string user, string password, string email)
        {
            if (!userExists(user))
            {
                int saltLength = 8;
                string salt = generateSalt(saltLength);
                string hashedPassword = this.hasher.computeSha256Hash(password + salt);

                if (this.allUsersDeets != null)
                {
                    this.allUsersDeets[user] = new Dictionary<string, string>();
                    this.allUsersDeets[user]["hashedPassword"] = hashedPassword;
                    this.allUsersDeets[user]["salt"] = salt;
                    this.allUsersDeets[user]["email"] = email;
                }

                return true;
            }

            return false;
        }

        public bool userExists(string user)
        {
            return this.allUsersDeets.ContainsKey(user);
        }

        public bool updatePassword(string userOrEmail, string newPassword)
        {
            int saltLength = 8;

            if (RandomFunctions.isValidEmail(userOrEmail))
            {
                foreach(string user in  this.allUsersDeets.Keys)
                {
                    if (userOrEmail.CompareTo(this.allUsersDeets[user]) == 0)
                    {
                        this.allUsersDeets[user]["salt"] = this.generateSalt(saltLength);
                        this.allUsersDeets[user]["hashedPassword"] = this.hasher.computeSha256Hash(newPassword + this.allUsersDeets[user]["salt"]);
                        return true;
                    }
                }
            }
            else
            {
                if (this.allUsersDeets.ContainsKey(userOrEmail))
                {
                    this.allUsersDeets[userOrEmail]["salt"] = this.generateSalt(saltLength);
                    this.allUsersDeets[userOrEmail]["hashedPassword"] = this.hasher.computeSha256Hash(newPassword + this.allUsersDeets[userOrEmail]["salt"]);
                    return true;
                }
            }

            return false;
        }

        public Dictionary<string, string> getUserDetails(string user)
        {
            return this.allUsersDeets[user];
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
