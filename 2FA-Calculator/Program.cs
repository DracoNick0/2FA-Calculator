using _2FA_Calculator.ClientSide;


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
        LoginInterface userLogin = new LoginInterface();

        switch (userInput[0])
        {
            case '1':
                if (userLogin.login())
                {
                    Console.WriteLine("\nWelcome " + userLogin.Username());

                    CalculatorClass calculator = new CalculatorClass();
                    Console.WriteLine("Input a simple expression with two integers and an operator, no spaces.");

                    double? temp;
                    if ((temp = calculator.evaluateExpression(Console.ReadLine())) != null)
                    {
                        Console.WriteLine("Result = " + temp.ToString() + "\n");
                    }
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