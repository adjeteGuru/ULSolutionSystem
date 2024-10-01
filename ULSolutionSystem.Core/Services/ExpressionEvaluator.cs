using ULSolutionSystem.Core.ExceptionHandler;
using ULSolutionSystem.Core.Extensions;
using ULSolutionSystem.Core.Services.Contracts;

namespace ULSolutionSystem.Core.Services
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        /// <inheritdoc/>
        public double Evaluate(string expression)
        {
            var valuesRecorded = new Stack<double>();
            var operatorsRecorded = new Stack<char>();

            bool isValid = ExpressionHandlerExtension.ExpressionValidator(expression);

            if (!isValid)
            {               
                throw new ExpressionException("Invalid expression, it contains negative number or parathensis.");
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

        /// <summary>
        /// Determines the precedence of the given operator.
        /// </summary>
        /// <param name="op">The operator whose precedence is to be determined.</param>
        /// <returns>An integer representing the precedence of the operator.</returns>
        private int Precedence(char op)
        {
            return op == '+' || op == '-' ? 1 : 2;
        }

        /// <summary>
        /// Applies the given operator to two operands and returns the result.
        /// </summary>
        /// <param name="op">The operator to apply.</param>
        /// <param name="b">The second operand.</param>
        /// <param name="a">The first operand.</param>
        /// <returns>The result of applying the operator to the operands.</returns>
        /// <exception cref="ExpressionException">Thrown when an invalid operator is encountered.</exception>
        private double ApplyOperator(char op, double b, double a)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                _ => throw new ExpressionException("Invalid operator found.")
            };
        }
    }
}