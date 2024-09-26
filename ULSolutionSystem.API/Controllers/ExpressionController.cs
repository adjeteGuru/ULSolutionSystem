using Microsoft.AspNetCore.Mvc;

namespace ULSolutionSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpressionController : ControllerBase
    {
        public ExpressionController()
        {

        }

        [HttpGet]
        public IActionResult ProcessEvaluation([FromQuery] string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return BadRequest("Expression cannot be null or empty.");
            }
            return Ok(default);
        }
    }
}
