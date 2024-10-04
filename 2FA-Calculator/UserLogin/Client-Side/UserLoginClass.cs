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

        public void requestUserAndPass()
        {
            Console.Write("Input username,password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                string[] tokens = userInput.Split(',');

                if (tokens.Length == 2)
                {
                    this.inputtedUsername = tokens[0];
                    this.inputtedPassword = tokens[1];
                }
                else
                {
                    requestUserAndPass();
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

        private void requestUsername()
        {
            Console.Write("Input username: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                this.inputtedUsername = userInput;
            }
            else
            {
                requestUsername();
            }
        }

        private void requestPassword()
        {
            Console.Write("Input password: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                this.inputtedPassword = userInput;
            }
            else
            {
                requestPassword();
            }
        }
        
        public bool authenticateUser()
        {
            UserAuthenticationClass
            return false;
        }

        // Put this in a different file so that we can salt it.
        public void createAccount(string username, string password)
        {

        }
    }
}
