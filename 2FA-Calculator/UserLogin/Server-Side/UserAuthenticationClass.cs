using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace _2FA_Calculator.UserLogin
{
    class UserAuthenticationClass
    {
        public UserAuthenticationClass() { }

        // Put this in a different file and add security.
        public bool UserCredentialsAuthentication(string username, string password)
        {
            string userLoginStorage = "UserLoginStorage.txt";
            string? line = string.Empty;

            using (StreamReader sr = new StreamReader(userLoginStorage))
            {
                while((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    string saltedPasswordHash = ComputeSha256Hash(password + tokens[2]);
                    if (tokens[0].CompareTo(username) == 0 && tokens[1].CompareTo(saltedPasswordHash) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Put this in a different file so that we can salt it.
        public void createAccount(string username, string password)
        {
            string userLoginStorage = "UserLoginStorage.txt";

            using (StreamWriter sw = new StreamWriter(userLoginStorage, true))
            {
                int saltLength = 8;
                string salt = GenerateSalt(saltLength);

                string hashedPassword = ComputeSha256Hash(password + salt);

                string line = username + "," + hashedPassword + "," + salt;
                sw.WriteLine(line);
            }
        }

        private string GenerateSalt(int length)
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

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 object
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash - returns a byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));  // Convert byte to hex string
                }
                return builder.ToString();
            }
        }
    }
}
