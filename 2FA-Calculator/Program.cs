using _2FA_Calculator.UserLogin.ClientSide;

Console.WriteLine("Please input number corresponding to option: ");
Console.WriteLine("  1. Login");
Console.WriteLine("  2. Create new account");

string? userInput = Console.ReadLine();
if (userInput != null)
{
    UserLoginClass userLogin = new UserLoginClass();

    switch (userInput[0])
    {
        case '1':
            userLogin.requestUserAndPass();
            if (userLogin.authenticateUser())
            {
                Console.WriteLine("Username: " + userLogin.getUsername());
                Console.WriteLine("Password: " + userLogin.getPassword());
                Console.WriteLine("You're in! :)");
            }
            else
            {
                Console.WriteLine("Username: " + userLogin.getUsername());
                Console.WriteLine("Password: " + userLogin.getPassword());
                Console.WriteLine("You're not in! :(");
            }
            break;
        case '2':
            userLogin.createAccount();
            break;
    }
}
