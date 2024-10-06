﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2FA_Calculator.UserLogin.ServerSide;

namespace _2FA_Calculator.UserLogin.ClientSide
{
    class UserLoginClass
    {
        private UserAuthenticator userAuth;
        private UserManager userManager;
        private string inputtedUsername;
        private string inputtedPassword;

        public UserLoginClass()
        {
            this.userAuth = new UserAuthenticator();
            this.userManager = new UserManager();
            this.inputtedUsername = string.Empty;
            this.inputtedPassword = string.Empty;
        }

        public string getUsername()
        {
            return this.inputtedUsername;
        }

        public string getPassword()
        {
            return this.inputtedPassword;
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
                    Console.WriteLine("\nIncorrect input, try again.");
                    requestUserAndPass();
                }
            }
        }

        // Put this in a different file so that we can salt it.
        public void createAccount()
        {
            this.inputtedUsername = requestInputFromUserAndConfirm("username");
            this.inputtedPassword = requestInputFromUserAndConfirm("password");

            // Go through server to create account
            this.userManager.createAccount(this.inputtedUsername, this.inputtedPassword);
        }

        public bool authenticateUser()
        {
            return this.userAuth.authenticateUser(this.inputtedUsername, this.inputtedPassword);
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
