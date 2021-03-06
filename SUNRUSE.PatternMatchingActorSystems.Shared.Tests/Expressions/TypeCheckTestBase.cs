// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SUNRUSE.PatternMatchingActorSystems.Shared.Expressions
{
    public abstract class TypeCheckTestBase
    {
        protected abstract IExpression CreateInstance(IExpression operand);

        protected abstract IEnumerable<ExpressionMock.TestData> ReturnsTrueFor { get; }

        [Theory]
        [InlineData(ExpressionMock.TestData.Null)]
        [InlineData(ExpressionMock.TestData.EmptyString)]
        [InlineData(ExpressionMock.TestData.NonEmptyString)]
        [InlineData(ExpressionMock.TestData.EmptyArray)]
        [InlineData(ExpressionMock.TestData.NonEmptyArray)]
        [InlineData(ExpressionMock.TestData.False)]
        [InlineData(ExpressionMock.TestData.True)]
        [InlineData(ExpressionMock.TestData.EmptyMap)]
        [InlineData(ExpressionMock.TestData.NonEmptyMap)]
        [InlineData(ExpressionMock.TestData.Guid)]
        [InlineData(ExpressionMock.TestData.ZeroInteger)]
        [InlineData(ExpressionMock.TestData.PositiveInteger)]
        [InlineData(ExpressionMock.TestData.LargePositiveInteger)]
        [InlineData(ExpressionMock.TestData.NegativeInteger)]
        [InlineData(ExpressionMock.TestData.LargeNegativeInteger)]
        public void ToExpressionBody(ExpressionMock.TestData testData)
        {
            var expression = CreateInstance(new ExpressionMock(ExpressionMock.TestDataValues[testData]));

            var expressionBody = expression.ToExpressionBody();

            var wrapped = Expression.Lambda<Func<object>>(expressionBody);
            var compiled = wrapped.Compile();
            var evaluated = compiled();
            Assert.Equal(ReturnsTrueFor.Contains(testData), evaluated);
        }

        [Theory]
        [InlineData(ExpressionMock.TestData.Null)]
        [InlineData(ExpressionMock.TestData.EmptyString)]
        [InlineData(ExpressionMock.TestData.NonEmptyString)]
        [InlineData(ExpressionMock.TestData.EmptyArray)]
        [InlineData(ExpressionMock.TestData.NonEmptyArray)]
        [InlineData(ExpressionMock.TestData.False)]
        [InlineData(ExpressionMock.TestData.True)]
        [InlineData(ExpressionMock.TestData.EmptyMap)]
        [InlineData(ExpressionMock.TestData.NonEmptyMap)]
        [InlineData(ExpressionMock.TestData.Guid)]
        [InlineData(ExpressionMock.TestData.ZeroInteger)]
        [InlineData(ExpressionMock.TestData.PositiveInteger)]
        [InlineData(ExpressionMock.TestData.LargePositiveInteger)]
        [InlineData(ExpressionMock.TestData.NegativeInteger)]
        [InlineData(ExpressionMock.TestData.LargeNegativeInteger)]
        public void ToExpressionBodyTypeObject(ExpressionMock.TestData testData)
        {
            var expression = CreateInstance(new ExpressionMock(ExpressionMock.TestDataValues[testData]));

            var expressionBody = expression.ToExpressionBody();

            Assert.Equal(typeof(object), expressionBody.Type);
        }

        [Fact]
        public void ToExpressionBodyMismatch()
        {
            var expression = CreateInstance(new ExpressionMock(default(Mismatch)));

            var expressionBody = expression.ToExpressionBody();

            var wrapped = Expression.Lambda<Func<object>>(expressionBody);
            var compiled = wrapped.Compile();
            var evaluated = compiled();
            Assert.IsType<Mismatch>(evaluated);
        }

        [Fact]
        public void ToExpressionBodyMismatchTypeObject()
        {
            var expression = CreateInstance(new ExpressionMock(default(Mismatch)));

            var expressionBody = expression.ToExpressionBody();

            Assert.Equal(typeof(object), expressionBody.Type);
        }
    }
}