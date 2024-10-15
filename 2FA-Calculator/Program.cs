﻿using _2FA_Calculator.ClientSide;


string? userInput = null;
while (true)
{
    //Email2FA email2FA = new Email2FA();
    //email2FA.sendOTPEmail("santos_nick@outlook.com");

    Console.WriteLine("Please input number corresponding to option: ");
    Console.WriteLine("  1. Login");
    Console.WriteLine("  2. Create new account");
    Console.WriteLine("  3. Exit");

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        LoginInterface userLogin = new LoginInterface();

        switch (userInput[0])
        {
            case '1':
                userLogin.login();
                break;
            case '2':
                userLogin.createAccount();
                break;
            case '3':
                return 1;
        }
    }
}