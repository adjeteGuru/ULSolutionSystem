using FluentAssertions;
using ULSolutionSystem.Core.Services;

namespace ULSolutionSystem.Core.Tests.ServicesTests
{
    public class GivenTheExpressionEvaluatorIsCalled
    {
        private ExpressionEvaluator systemUnderTest;

        public GivenTheExpressionEvaluatorIsCalled()
        {
            systemUnderTest = new ExpressionEvaluator();
        }

        [Fact]
        public void Evaluate_WhenAParameterIsSuppliedAndTheExpressionIsEvaluated_ThenNoExceptionIsThrown()
        {
            var expression = "1";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().NotThrow();
        }

        [Fact]
        public void Evaluate_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe2()
        {
            var expression = "1+1";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(2);
        }

        [Fact]
        public void Evaluate_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe15()
        {
            var expression = "1+2*7";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(15);
        }

        [Fact]
        public void Evaluate_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe14()
        {
            var expression = "4+5*2";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(14);
        }

        [Fact]
        public void Evaluate_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe6dot5()
        {
            var expression = "4+5/2";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(6.5);
        }

        [Fact]
        public void Evaluate_WhenAWrongParameterIsSuppliedAndTheExpressionIsEvaluated_ThenExpectedErrorIsReturned()
        {
            var expression = "wrong input";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ArgumentException>("Invalid operator found.");
        }

        [Fact]
        public void Evaluate_WhenAParameterSuppliesCountainUnknowCharThenTheExpectedErrorIsThrown()
        {
            var expression = "1&2*6";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ArgumentException>("Invalid operator found.");
        }
    }
}
