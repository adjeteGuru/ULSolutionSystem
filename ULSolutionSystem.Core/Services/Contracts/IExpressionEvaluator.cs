namespace ULSolutionSystem.Core.Services.Contracts
{
    /// <summary>
    /// Interface for the Expression Evaluator service.
    /// </summary>
    public interface IExpressionEvaluator
    {
        /// <summary>
        /// Evaluates a mathematical expression given as a string.
        /// The expression can include non-negative integers,
        /// the operators +, -, *, /, and parentheses
        /// </summary>
        /// <param name="expression">The mathematical expression to evaluate.</param>
        /// <returns>The result of the evaluated expression as a double.</returns>
        /// <exception cref="ExpressionException">Thrown when the expression is invalid.</exception>
        double Evaluate(string expression);
    }
}