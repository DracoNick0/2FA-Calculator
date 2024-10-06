using System;
using _2FA_Calculator.Server;

namespace _2FA_Calculator.Client
{
    class UserLoginClass
    {
        private UserAuthenticator userAuth;
        private UserManager userManager;
        private string inputtedUsername;
        private string inputtedPassword;

        public UserLoginClass()
        {
            userAuth = new UserAuthenticator();
            userManager = new UserManager();
            inputtedUsername = string.Empty;
            inputtedPassword = string.Empty;
        }

        public string getUsername()
        {
            return inputtedUsername;
        }

        public string getPassword()
        {
            return inputtedPassword;
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
                    inputtedUsername = tokens[0];
                    inputtedPassword = tokens[1];
                }
                else
                {
                    Console.WriteLine("\nIncorrect input, try again.");
                    requestUserAndPass();
                }
            }
        }

        // Put this in a different file so that we can salt it.
        public void createAccount()
        {
            inputtedUsername = requestInputFromUserAndConfirm("username");
            inputtedPassword = requestInputFromUserAndConfirm("password");

            // Go through server to create account
            userManager.createAccount(inputtedUsername, inputtedPassword);
        }

        public bool authenticateUser()
        {
            return userAuth.authenticateUser(inputtedUsername, inputtedPassword);
        }

        private string requestInputFromUserAndConfirm(string nameOfInput)
        {
            bool userIsSatisfied = false;
            string? userInput = null;

            while (!userIsSatisfied || userInput == null || userInput == string.Empty)
            {
                while (userInput == null || userInput == string.Empty)
                {
                    Console.Write("Enter your desired " + nameOfInput + ": ");
                    userInput = Console.ReadLine();

                    // Check if username already exists ********************************************************
                    if (userInput == null || userInput == string.Empty)
                    {
                        Console.WriteLine("\n" + nameOfInput + " not valid, try again.");
                    }
                }

                do
                {
                    Console.WriteLine("Is \"" + userInput + "\" correct?");
                } while (!(userIsSatisfied = assessYesNoInput(Console.ReadLine())));
            }

            return userInput;
        }

        private void requestUsername()
        {
            Console.Write("Input username: ");

            string? userInput = null;
            if ((userInput = Console.ReadLine()) != null)
            {
                inputtedUsername = userInput;
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
                inputtedPassword = userInput;
            }
            else
            {
                requestPassword();
            }
        }

        private bool assessYesNoInput(string? input)
        {
            if (input != null)
            {
                if (input.Length == 1)
                {
                    if (input.CompareTo("y") == 0 || input.CompareTo("Y") == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
