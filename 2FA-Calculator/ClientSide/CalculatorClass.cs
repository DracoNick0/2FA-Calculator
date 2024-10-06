namespace _2FA_Calculator.ClientSide
{
    class CalculatorClass
    {
        public CalculatorClass() { }

        public double? evaluateExpression(string? expression)
        {
            // Assuuming the expression is 3 things:
            // 2 Integers and an operator

            if (expression != null)
            {
                int int1 = getFirstIntInString(ref expression);
                char op = expression[0];
                expression = expression.Substring(1);
                int int2 = getFirstIntInString(ref expression);

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

        public int getFirstIntInString(ref string expression)
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
