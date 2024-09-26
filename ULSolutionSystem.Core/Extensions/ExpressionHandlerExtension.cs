using System.Text;
using System.Text.RegularExpressions;

namespace ULSolutionSystem.Core.Extensions
{
    public static class ExpressionHandlerExtension
    {
        public static List<string> TokenFormatter(this string expression)
        {
            var tokens = new List<string>();
            var number = string.Empty;
            var currentToken = new StringBuilder();

            foreach (var c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    currentToken.Append(c);
                }
                else
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    tokens.Add(c.ToString());
                }
            }

            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }

            return tokens;
        }

        public static bool ExpressionValidator(this string expression)
        {           
            if (expression.Contains('(') || expression.Contains(')'))
            {
                return false;
            }
           
            string negativeNumberPattern = @"-\d";
            if (Regex.IsMatch(expression, negativeNumberPattern))
            {
                return false;
            }

            return true;
        }

    }
}
