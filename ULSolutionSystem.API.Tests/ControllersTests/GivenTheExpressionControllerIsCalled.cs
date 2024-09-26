using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ULSolutionSystem.API.Controllers;

namespace ULSolutionSystem.API.Tests.ControllersTests
{
    public class GivenTheExpressionControllerIsCalled
    {
        private ExpressionController systemUnderTest;

        public GivenTheExpressionControllerIsCalled()
        {
            systemUnderTest = new ExpressionController();
        }

        [Fact]
        public void ProcessEvaluation_WhenAParameterIsSuppliedAndTheExpressionIsEvaluated_ThenNoExceptionIsThrown()
        {
            var expression = "1";
            var act = () => systemUnderTest.ProcessEvaluation(expression);
            act.Should().NotThrow();
        }

        [Fact]
        public void ProcessEvaluation_WhenAParameterIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultTypeIsReturned()
        {
            var expression = "1";           
            var act = systemUnderTest.ProcessEvaluation(expression);
            act.Should().BeOfType<OkObjectResult>();
        }
    }
}
