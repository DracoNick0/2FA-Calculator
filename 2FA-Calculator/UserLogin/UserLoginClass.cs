using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2FA_Calculator.UserLogin
{
    class UserLoginClass
    {
        private string inputtedUsername;
        private string inputtedPassword;

        public UserLoginClass()
        {
            inputtedUsername = string.Empty;
            inputtedPassword = string.Empty;
        }

        public void RequestUserAndPass()
        {
            Console.Write("Input username,password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                string[] tokens = userInput.Split(',');
                
                if(tokens.Length == 2)
                {
                    this.inputtedUsername = tokens[0];
                    this.inputtedPassword = tokens[1];
                }
                else
                {
                    RequestUserAndPass();
                }
            }
        }

        public string getUsername()
        {
            return this.inputtedUsername;
        }

        public string getPassword()
        {
            return this.inputtedPassword;
        }

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

        public void createAccount(string username, string password)
        {
            string userLoginStorage = "UserLoginStorage.txt";

            using (StreamWriter sw = new StreamWriter(userLoginStorage, true))
            {
                string line = username + "," + password;
                sw.WriteLine(line);
            }
        }

        private void RequestUsername()
        {
            Console.Write("Input username: ");

            string? userInput = null;
            if((userInput = Console.ReadLine()) != null)
            {
                this.inputtedUsername = userInput;
            }
            else
            {
                RequestUsername();
            }
        }

        private void RequestPassword()
        {
            Console.Write("Input password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                this.inputtedPassword = userInput;
            }
            else
            {
                RequestPassword();
            }
        }
    }
}
