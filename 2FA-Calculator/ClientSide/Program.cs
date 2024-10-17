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
                if (userLogin.login())
                {
                    Console.Clear();
                    CalculatorClass calculator = new CalculatorClass();
                    Console.WriteLine("Welcome " + userLogin.Username() + "!\n");
                    calculator.interactWithCalculator();
                }
                break;
            case '2':
                userLogin.createAccount();
                break;
            case '3':
                userLogin.forgotLogin();
                break;
            case '4':
                return 1;
        }
    }
}