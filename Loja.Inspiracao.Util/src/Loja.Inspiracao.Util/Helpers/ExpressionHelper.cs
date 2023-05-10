﻿#nullable disable

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Loja.Inspiracao.Util.Helpers
{
    public static class ExpressionHelper
    {
        public static List<Expression<Func<T, bool>>> CreateExpression<T>()
        {
            return new List<Expression<Func<T, bool>>>();
        }

        public static Expression<Func<T, bool>> JoinExpressions<T>(IEnumerable<Expression<Func<T, bool>>> filters)
        {
            Expression<Func<T, bool>> lambda = null;

            foreach (var filtro in filters)
            {
                lambda = lambda != null ? lambda.AndAlso(filtro) : filtro;
            }

            return lambda;
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);

        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                return node == _oldValue ? _newValue : base.Visit(node);
            }
        }
    }
}
