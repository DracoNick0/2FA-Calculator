﻿using _2FA_Calculator.ClientSide;

Console.WriteLine("Please input number corresponding to option: ");
Console.WriteLine("  1. Login");
Console.WriteLine("  2. Create new account");

string? userInput = Console.ReadLine();
if (userInput != null)
{
    LoginInterface userLogin = new LoginInterface();

    switch (userInput[0])
    {
        case '1':
            userLogin.requestUserAndPass();
            if (userLogin.authenticateUser())
            {
                Console.WriteLine("Username: " + userLogin.getUsername());
                Console.WriteLine("Password: " + userLogin.getPassword());
                Console.WriteLine("You're in! *v*");
            }
            else
            {
                Console.WriteLine("Username: " + userLogin.getUsername());
                Console.WriteLine("Password: " + userLogin.getPassword());
                Console.WriteLine("Incorrect username or password! ;n;");
            }
            break;
        case '2':
            userLogin.createAccount();
            break;
    }
}
