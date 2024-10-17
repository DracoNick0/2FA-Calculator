namespace _2FA_Calculator.ClientSide
{
    class CalculatorClass
    {
        public CalculatorClass() { }

        public void InteractWithCalculator()
        {
            while (true)
            {
                Console.WriteLine("Please select from the options:");
                Console.WriteLine("  1. Use calculator.");
                Console.WriteLine("  2. How to use calculator?");
                Console.WriteLine("  3. Exit.");

                string? userInput = Console.ReadLine();
                if (userInput != null)
                {
                    switch (userInput)
                    {
                        case "1":
                            double? result;
                            Console.WriteLine("Enter your simple expression: ");

                            if ((result = EvaluateExpression(Console.ReadLine())) != null)
                            {
                                Console.WriteLine("Result = " + result.ToString() + "\n");
                            }
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("How to use the calculator:");
                            Console.WriteLine("  1. Enter a simple expression with two integers and an operator, no spaces.");
                            Console.WriteLine("  2. Press enter.");
                            Console.WriteLine("    - eg. \"10*15\", \"7^3\", \"2024/100\"\n");
                            break;
                        case "3":
                            return;
                        default:
                            break;
                    }
                }
            }
        }

        private double? EvaluateExpression(string? expression)
        {
            // Assuuming the expression is 3 things:
            // 2 Integers and an operator

            if (expression != null)
            {
                int int1 = GetFirstIntInString(ref expression);
                char op = expression[0];
                expression = expression.Substring(1);
                int int2 = GetFirstIntInString(ref expression);

                switch (op)
                {
                    case '+':
                        return int1 + int2;
                    case '-':
                        return int1 - int2;
                    case '*':
                        return int1 * int2;
                    case '/':
                        return int1 / int2;
                    case '^':
                        return Math.Pow(int1, int2);
                }
            }

            return null;
        }

        private int GetFirstIntInString(ref string expression)
        {
            int i = 0;
            string integer = string.Empty;

            while (i < expression.Length && char.IsDigit(expression[i]))
            {
                integer = integer + expression[i];
                i++;
            }

            expression = expression.Substring(i);

            return int.Parse(integer);
        }
    }
}
