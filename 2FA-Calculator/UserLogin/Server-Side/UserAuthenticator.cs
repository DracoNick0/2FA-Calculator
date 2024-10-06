using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace _2FA_Calculator.UserLogin.ServerSide
{
    class UserAuthenticator
    {
        public UserAuthenticator() { }

        // Put this in a different file and add security.
        public bool authenticateUser(string username, string password)
        {
            string filePath = @"../../../UserLogin/Server-Side/UserCredentialsStorage.txt";
            string? line = string.Empty;
            Hasher hasher = new Hasher();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while((line = sr.ReadLine()) != null)
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
