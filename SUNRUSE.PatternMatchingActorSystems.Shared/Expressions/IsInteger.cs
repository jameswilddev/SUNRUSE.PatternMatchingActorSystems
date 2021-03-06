// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SUNRUSE.PatternMatchingActorSystems.Shared.Expressions
{
    /// <summary>A <see cref="IExpression" /> which returns <see langword="true" /> when its operand is a <see cref="int" />, else, <see langword="false" />.</summary>
    public sealed class IsInteger : IExpression
    {
        /// <summary>The <see cref="IExpression" /> to typecheck.</summary>
        public readonly IExpression Integer;

        /// <inheritdoc />
        public IsInteger(IExpression integer)
        {
            Integer = integer;
        }

        /// <inheritdoc />
        public Expression ToExpressionBody()
        {
            var integerExpressionBody = Integer.ToExpressionBody();
            return Expression.Condition
            (
                Expression.TypeIs(integerExpressionBody, typeof(Mismatch)),
                integerExpressionBody,
                Expression.Convert(Expression.TypeIs(integerExpressionBody, typeof(int)), typeof(object))
            );
        }
    }
}