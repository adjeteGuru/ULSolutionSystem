using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
