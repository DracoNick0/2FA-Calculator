using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2FA_Calculator.UserLogin
{
    class UserAuthenticationClass
    {
        public UserAuthenticationClass() { }

        public void RequestCredentials(string userInput)
        {
            
        }

        // Put this in a different file and add security.
        public bool isCorrect()
        {
            string userLoginStorage = "UserLoginStorage.txt";
            string? line = string.Empty;

            using (StreamReader sr = new StreamReader(userLoginStorage))
            {
                while((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    if (tokens[0].CompareTo(tokens[1]) == 0)
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
                string line = username + "," + password;
                sw.WriteLine(line);
            }
        }
    }
}
