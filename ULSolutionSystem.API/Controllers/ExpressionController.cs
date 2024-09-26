using Microsoft.AspNetCore.Mvc;
using ULSolutionSystem.Core.Services.Contracts;

namespace ULSolutionSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpressionController : ControllerBase
    {
        private readonly IExpressionEvaluator evaluator;

        public ExpressionController(IExpressionEvaluator evaluator)
        {
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }

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
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
