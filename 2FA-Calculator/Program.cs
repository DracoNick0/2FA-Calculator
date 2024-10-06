using _2FA_Calculator.ClientSide;


string? userInput = null;
while (true)
{
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
                userLogin.requestUserAndPass();
                if (userLogin.authenticateUser())
                {
                    Console.WriteLine("\nWelcome " + userLogin.getUsername());

                    CalculatorClass calculator = new CalculatorClass();
                    Console.WriteLine("Input a simple expression with two integers and an operator, no spaces.");

                    double? temp;
                    if ((temp = calculator.evaluateExpression(Console.ReadLine())) != null)
                    {
                        Console.WriteLine("Result = " + temp.ToString() + "\n");
                    }
                }
                else
                {
                    Console.WriteLine("Username: " + userLogin.getUsername());
                    Console.WriteLine("Password: " + userLogin.getPassword());
                    Console.WriteLine("Incorrect username and/or password! ;n;\n");
                }
                break;
            case '2':
                userLogin.createAccount();
                break;
            case '3':
                return 1;
        }
    }
}