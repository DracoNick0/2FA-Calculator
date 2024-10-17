using System.Text.RegularExpressions;

namespace _2FA_Calculator.ServerSide
{
    class RandomFunctions
    {
        static public bool IsValidEmail(string input)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(input, pattern);
        }
    }
}
