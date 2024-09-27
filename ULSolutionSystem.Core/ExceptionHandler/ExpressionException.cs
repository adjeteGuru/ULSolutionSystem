namespace ULSolutionSystem.Core.ExceptionHandler
{
    public class ExpressionException : Exception
    {
        public ExpressionException() : base("The stack is empty. Operation cannot be performed.")
        {

        }

        public ExpressionException(string message) : base(message)
        {

        }

        public ExpressionException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
