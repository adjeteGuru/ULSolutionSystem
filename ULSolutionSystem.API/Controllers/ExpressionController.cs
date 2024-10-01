using Microsoft.AspNetCore.Mvc;
using ULSolutionSystem.Core.ExceptionHandler;
using ULSolutionSystem.Core.Services.Contracts;

namespace ULSolutionSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpressionController : ControllerBase
    {
        private readonly IExpressionEvaluator evaluator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionController"/> class.
        /// </summary>
        /// <param name="evaluator">The expression evaluator to use for evaluating expressions.</param>
        /// <exception cref="ArgumentNullException">Thrown when the evaluator is null.</exception>
        public ExpressionController(IExpressionEvaluator evaluator)
        {
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }


        /// <summary>
        /// Processes the evaluation of a mathematical expression.
        /// </summary>
        /// <param name="expression">The mathematical expression to evaluate.</param>
        /// <returns>An <see cref="IActionResult"/> containing the result of the evaluation or an error message.</returns>
        [HttpGet]
        public IActionResult ProcessEvaluation([FromQuery] string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return BadRequest("Expression cannot be null or empty.");
            }

            try
            {
                var result = evaluator.Evaluate(expression);
                return Ok(result);
            }
            catch (ExpressionException ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
