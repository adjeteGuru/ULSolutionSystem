using System.Text;

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

    }
}
