using _2FA_Calculator.ClientSide;

LoginInterface userLogin = new LoginInterface();

string? userInput = null;
while (true)
{
    Console.WriteLine("Please input number corresponding to option: ");
    Console.WriteLine("  1. Login");
    Console.WriteLine("  2. Create new account");
    Console.WriteLine("  3. Forgot login");
    Console.WriteLine("  4. Exit");

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        switch (userInput[0])
        {
            case '1':
                Console.Clear();
                if (userLogin.Login()) // Ask users for credentials to access calculator.
                {
                    Console.Clear();

                    CalculatorClass calculator = new CalculatorClass();
                    Console.WriteLine("Welcome " + userLogin.Username() + "!\n");
                    calculator.InteractWithCalculator();
                }
                break;
            case '2':
                Console.Clear();
                userLogin.CreateAccount();
                break;
            case '3':
                Console.Clear();
                userLogin.ForgotLogin();
                break;
            case '4':
                Console.Clear();
                userLogin.SaveAllUsersCredentials(); // Tell the server to save all credentials.
                return 1;
            default:
                Console.Clear();
                Console.WriteLine("Not a valid input, try again.\n");
                break;
        }
    }
}