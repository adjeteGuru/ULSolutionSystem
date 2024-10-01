using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ULSolutionSystem.API.Controllers;
using ULSolutionSystem.Core.ExceptionHandler;
using ULSolutionSystem.Core.Services.Contracts;

namespace ULSolutionSystem.API.Tests.ControllersTests
{
    public class GivenTheExpressionControllerIsCalled
    {
        private Mock<IExpressionEvaluator> mockExpressionEvaluator;
        private ExpressionController systemUnderTest;

        public GivenTheExpressionControllerIsCalled()
        {
            mockExpressionEvaluator = new Mock<IExpressionEvaluator>();
            systemUnderTest = new ExpressionController(mockExpressionEvaluator.Object);
        }

        [Fact]
        public void Constructor_WhenANullParamIsSupplies_ThenTheExpectedExceptionIsThrown()
        {
            var act = () => new ExpressionController(null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("evaluator");
        }

        [Fact]
        public void Constructor_WhenAValidParamIsSupplies_ThenNoExceptionIsThrown()
        {
            var act = () => new ExpressionController(mockExpressionEvaluator.Object);
            act.Should().NotThrow();
        }

        [Fact]
        public void ProcessEvaluation_WhenAnEmptyParameterIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultTypeIsReturned()
        {
            mockExpressionEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(null);
            var act = systemUnderTest.ProcessEvaluation("");
            act.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void ProcessEvaluation_WhenAnEmptyParameterIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedErrorMessageIsReturned()
        {
            mockExpressionEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(null);
            var result = systemUnderTest.ProcessEvaluation("");
            var objectResult = result as BadRequestObjectResult;
            objectResult.StatusCode.Should().Be(400, "Expression cannot be null or empty.");
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

        [Fact]
        public void ProcessEvaluation_WhenAParameterIsSupplied_ThenExpressionEvaluatorIsInvoked()
        {
            var expression = "1";
            systemUnderTest.ProcessEvaluation(expression);
            mockExpressionEvaluator.Verify(x => x.Evaluate(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ProcessEvaluation_WhenAnInvalidParameterIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedErrorMessageIsReturned()
        {
            var exception = new ExpressionException("Invalid expression.");
            mockExpressionEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Throws(exception);
            
            var result = systemUnderTest.ProcessEvaluation("4+5/2-1");
            var objectResult = result as BadRequestObjectResult;
            objectResult.Value.Should().Be($"{exception.Message}");
        }

        [Fact]
        public void ProcessEvaluation_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe14()
        {
            var expression = "4+5*2";
            mockExpressionEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(14);
            
            var result = systemUnderTest.ProcessEvaluation(expression);
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(200, "14");
        }
    }
}
