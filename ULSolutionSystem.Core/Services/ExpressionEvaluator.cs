using ULSolutionSystem.Core.ExceptionHandler;
using ULSolutionSystem.Core.Extensions;
using ULSolutionSystem.Core.Services.Contracts;

namespace ULSolutionSystem.Core.Services
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        public double Evaluate(string expression)
        {
            var valuesRecorded = new Stack<double>();
            var operatorsRecorded = new Stack<char>();

            bool isValid = ExpressionHandlerExtension.ExpressionValidator(expression);

            if (!isValid)
            {
                throw new ArgumentException("Invalid expression, it contains negative number or parathensis.");
            }

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
                        if (valuesRecorded.Count < 2)
                        {
                            throw new ExpressionException("Invalid expression, it contains either a negative number or wrong expression.");
                        }

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
            return op == '+' || op == '-' ? 1 : 2;
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