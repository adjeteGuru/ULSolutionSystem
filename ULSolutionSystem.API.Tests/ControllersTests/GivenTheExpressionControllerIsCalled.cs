﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ULSolutionSystem.API.Controllers;
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
    }
}
