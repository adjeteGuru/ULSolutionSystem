
using ULSolutionSystem.Core.Extensions;

namespace ULSolutionSystem.Core.Services
{
    public class ExpressionEvaluator
    {
        public double Evaluate(string expression)
        {
            var valuesRecorded = new Stack<double>();
            var operatorsRecorded = new Stack<char>();

            var tokens = ExpressionHandlerExtension.TokenFormatter(expression);

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double value))
                {
                    valuesRecorded.Push(value);
                }
                else
                {
                    while (operatorsRecorded.Count > 0 && Precedence(operatorsRecorded.Peek()) >= Precedence(token[0]))
                    {
                        valuesRecorded.Push(ApplyOperator(operatorsRecorded.Pop(), valuesRecorded.Pop(), valuesRecorded.Pop()));
                    }
                    operatorsRecorded.Push(token[0]);
                }
            }

            while (operatorsRecorded.Count > 0)
            {
                valuesRecorded.Push(ApplyOperator(operatorsRecorded.Pop(), valuesRecorded.Pop(), valuesRecorded.Pop()));
            }

            return valuesRecorded.Pop();
        }

        private int Precedence(char op)
        {
            return op switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                _ => throw new ArgumentException("Invalid operator found."),
            };
        }

        private double ApplyOperator(char op, double b, double a)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                _ => throw new ArgumentException("Invalid operator found."),
            };
        }
    }
}