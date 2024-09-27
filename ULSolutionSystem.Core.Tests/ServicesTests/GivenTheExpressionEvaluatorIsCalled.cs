using FluentAssertions;
using ULSolutionSystem.Core.ExceptionHandler;
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
        public void Evaluate_WhenAValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe5dot5()
        {
            var expression = "4+5/2-1";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(5.5);
        }

        [Fact]
        public void Evaluate_WhenAComplexValidInputIsSuppliedAndTheExpressionIsEvaluated_ThenTheExpectedResultToBe3dot5()
        {
            var expression = "3*2-4+5/2-1";
            var result = systemUnderTest.Evaluate(expression);
            result.Should().Be(3.5);
        }

        [Fact]
        public void Evaluate_WhenAWrongParameterIsSuppliedAndTheExpressionIsEvaluated_ThenExpectedExceptionIsThrown()
        {
            var expression = "wrong input";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ExpressionException>().And.Message.Should().Be("Invalid expression, it contains either a negative number or wrong expression.");
        }

        [Fact]
        public void Evaluate_WhenAParameterSuppliesCountainUnknowCharThenTheExpectedErrorIsThrown()
        {
            var expression = "1&2*6";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ArgumentException>("Invalid operator found.");
        }

        [Fact]
        public void Evaluate_WhenAParameterSuppliesCountainsAParenthesis_ThenTheExpectedErrorIsThrown()
        {
            var expression = "(4+5)*2";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ArgumentException>("Invalid expression, it contains negative number or parathensis.");
        }

        [Fact]
        public void Evaluate_WhenAParameterSuppliesCountainANegativeNumber_ThenTheExpectedErrorIsThrown()
        {
            var expression = "3*-2";
            var act = () => systemUnderTest.Evaluate(expression);
            act.Should().Throw<ExpressionException>().And.Message.Should().Be("Invalid expression, it contains either a negative number or wrong expression.");
        }
    }
}
